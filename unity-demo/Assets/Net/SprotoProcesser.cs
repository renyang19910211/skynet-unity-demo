using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Sproto;
using UnityEngine;
using System;

//处理网络协议
public class SprotoProcesser
{
    private NetTransporter _transporter;
    private SprotoRpc _sprotoRpc;
    private SprotoRpc.RpcRequest _host;
    private Action<SprotoRpc.RpcInfo> _onEvent;
    

    public SprotoProcesser(Socket socket, Action<SprotoRpc.RpcInfo> onEvent)
    {
        _sprotoRpc = new SprotoRpc();
		_host = _sprotoRpc.Attach(Protocol.Instance);
        _onEvent = onEvent;

        _transporter = new NetTransporter(socket, OnReceive);
        _transporter.Start();
    }

    //发送sproto消息
    public void Send<T>(SprotoTypeBase obj = null, long? session = null)
    {
		byte[] req = _host.Invoke<T>(obj, session);
        ushort length = (ushort)req.Length;

        byte[] lengthBytes = BitConverter.GetBytes(length);
        byte[] bytes = new byte[length + 2];

        if (NetTransporter.IS_NEED_CONVERT_BIG_ENDING)
        {
            bytes[0] = lengthBytes[1];
            bytes[1] = lengthBytes[0];
        }
        
        for (int i = 0; i < req.Length; ++i)
            bytes[i + 2] = req[i];
        _transporter.Send(bytes);
    }

    //接收到网络消息
    private void OnReceive(byte[] bytes)
    {
        try
        {
            SprotoRpc.RpcInfo sinfo = _sprotoRpc.Dispatch(bytes);
            _onEvent.Invoke(sinfo);
        }
        catch(Exception e)
        {
            Debug.LogError(e);
        }
    }
}
