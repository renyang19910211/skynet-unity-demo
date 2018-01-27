using System.Collections;
using System.Collections.Generic;
using System;
using System.Net.Sockets;
using System.Net;
using UnityEngine;


//负责网络传输
public class NetTransporter
{
    public const bool IS_NEED_CONVERT_BIG_ENDING = true; //是否需要转换为大端序

    private const int MAX_READ = 1500;
    private const int HEAD_LENGTH = 2; //头长度

    private Socket _socket;
    private byte[] _headBuffer = new byte[HEAD_LENGTH];
    private byte[] _buffer = new byte[MAX_READ]; //用于接收

    private byte[] _byteBuffer; //用于缓存
    private int _bufferOffset = 0; //缓存偏移

    private Action<byte[]> _eventHandle; //消息处理回调

    public NetTransporter(Socket socket, Action<byte[]> eventHandle)
    {
        try
        {
            _socket = socket;
            _eventHandle = eventHandle;
        }
        catch (SocketException e)
        {
            Debug.LogError(e);
            return;
        }
    }

    public void Start()
    {
        _socket.BeginReceive(_buffer, 0, MAX_READ, SocketFlags.None, new AsyncCallback(EndReceive), null);
    }

    private void EndReceive(IAsyncResult result)
    {
        try
        {
            int byteLength = _socket.EndReceive(result);

            ProcessBytes(_buffer, byteLength, 0);
            Array.Clear(_buffer, 0, MAX_READ);
            _socket.BeginReceive(_buffer, 0, MAX_READ, SocketFlags.None, new AsyncCallback(EndReceive), null);
        }
        catch (SocketException e)
        {
            Debug.LogError(e);
            return;
        }
    }

    //处理接收的字节
    private void ProcessBytes(byte[] bytes, int length, int offset)
    {
        if (_byteBuffer == null)
            ReadHead(bytes, length, offset);
        else
            ReadBody(bytes, length, offset);
    }

    //写入缓存
    private void WriteBytes(byte[] bytes, int start, int length, int offset, byte[] target)
    {
        for (int i = 0; i < length; ++i)
            target[start + i] = bytes[i + offset];
    }

    //读取消息头
    private void ReadHead(byte[] bytes, int length, int offset)
    {
        //消息长度
        int bufferLength = length - offset;
        //当前消息头还需接收的字节
        int headLeft = HEAD_LENGTH - _bufferOffset; 

        //消息头已经接收完成, 开始解析消息头
        if (bufferLength >= headLeft)
        {
            WriteBytes(bytes, _bufferOffset, headLeft, offset, _headBuffer);
            //大端序转换一下
            if (IS_NEED_CONVERT_BIG_ENDING)
            {
                byte b = _headBuffer[0];
                _headBuffer[0] = _headBuffer[1];
                _headBuffer[1] = b;
            }

            offset += headLeft;
            _bufferOffset = 0;

            ushort bodyLength = BitConverter.ToUInt16(_headBuffer, 0);
            _byteBuffer = new byte[bodyLength];
            if (offset < length) ReadBody(bytes, length, offset);
        }
        //长度不足
        else
        {
            WriteBytes(bytes, _bufferOffset, bufferLength, offset, _headBuffer);
            _bufferOffset += bufferLength;
        }
    }

    //读取消息体
    private void ReadBody(byte[] bytes, int length, int offset)
    {
        //消息长度
        int bufferLength = length - offset;
        //当前消息体还需接收的字节
        int bodyLeft = _byteBuffer.Length - _bufferOffset;

        //消息体完整
        if (bufferLength >= bodyLeft)
        {
            WriteBytes(bytes, _bufferOffset, bodyLeft, offset, _byteBuffer);
            offset += bodyLeft;
            _bufferOffset = 0;

            //抛出消息
            if (_eventHandle != null) _eventHandle.Invoke(_byteBuffer);

            _byteBuffer = null;
            //继续接收
            if (offset < length) ReadHead(bytes, length, offset);
        }
        else
        {
            WriteBytes(bytes, _bufferOffset, bufferLength, offset, _byteBuffer);
            _bufferOffset += bufferLength;
        }
    }

    public void Send(byte[] bytes)
    {
        try
        {
            if (_socket != null && _socket.Connected)
                _socket.Send(bytes);
            else
                Debug.LogError("socket is disconnect");
        }
        catch (SocketException e)
        {
            Debug.LogError(e);
        }
    }
}
