using Sproto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class ClientNet : SingletonBehaviour<ClientNet>
{

    public static ClientNet _inst;

    private struct Event
    {
        public Action<SprotoTypeBase> callback;
        public SprotoTypeBase obj;

        public Event(Action<SprotoTypeBase> call, SprotoTypeBase o)
        {
            callback = call;
            obj = o;
        }
    }

    //会话id
    private long _session = 0;
    //response
    private Dictionary<long, Action<SprotoTypeBase>> _responseDict = new Dictionary<long, Action<SprotoTypeBase>>();
    //消息队列,使消息在主线程中抛出
    private Queue<Event> _eventQueue = new Queue<Event>();
    private Socket _socket;
    //使用sproto协议
    private SprotoProcesser _sprotoProcesser;
    private Action<SprotoTypeBase> _onConnect;

    //连接
    public void Connect(string ip, int port, Action<SprotoTypeBase> onConnect)
    {
        Thread thread = new Thread(new ThreadStart(() =>
        {
            _onConnect = onConnect;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            _socket.SendTimeout = 1000;
            _socket.ReceiveTimeout = 1000;
            _socket.BeginConnect(ipEndPoint, new AsyncCallback(OnConnect), null);
        }));
        thread.Start();
    }

    public void Send<TProto>(SprotoTypeBase obj = null, Action<SprotoTypeBase> callback = null)
    {
        if (_sprotoProcesser != null)
        {
            _sprotoProcesser.Send<TProto>(obj, _session);
            if (callback != null) _responseDict.Add(_session, callback);
            _session += 1;
        }
    }

    //收到消息
    private void OnEvent(SprotoRpc.RpcInfo sinfo)
    {
        //推送消息
        if (sinfo.type == SprotoRpc.RpcType.REQUEST)
        {
            _eventQueue.Enqueue(new Event(null, sinfo.requestObj));
        }
        //回返消息
        else
        {
            if (sinfo.session.HasValue)
            {
                Action<SprotoTypeBase> call;
                _responseDict.TryGetValue(sinfo.session.Value, out call);
                if (call != null) _eventQueue.Enqueue(new Event(call, sinfo.responseObj));
            }
        }
    }

    private void OnConnect(IAsyncResult result)
    {
        try
        {
            _socket.EndConnect(result);
            _sprotoProcesser = new SprotoProcesser(_socket, OnEvent);
            _eventQueue.Enqueue(new Event(_onConnect, null));
        }
        catch (SocketException e)
        {
            Debug.Log(e);
        }
    }

    private void Update()
    {
        while (_eventQueue.Count > 0)
        {
            Event evt = _eventQueue.Dequeue();
            if (evt.callback != null) evt.callback.Invoke(evt.obj);
            //推送消息
            else
            {

            }
        }
    }
}
