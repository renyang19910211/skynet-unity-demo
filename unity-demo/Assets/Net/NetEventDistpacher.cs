using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NetEventDistpacher : MonoBehaviour 
{
	public static NetEventDistpacher inst{
		get{
			if (_inst == null)
				_inst = GameObject.FindObjectOfType (typeof(NetEventDistpacher)) as NetEventDistpacher;
			return _inst;
		}
	}
	private static NetEventDistpacher _inst;

	private Queue<byte[]> _msgQueue = new Queue<byte[]>(); 
	private bool isOnConnect = false;
	private Action _onConnect;
	private Action<byte[]> _onSocketMsg;

	public void AddConnectCallback(Action callback)
	{
		_onConnect = callback;
	}

	public void AddSocketMsgCallback(Action<byte[]> callback)
	{
		_onSocketMsg = callback;
	}

	public void OnSocketMsg(byte[] bytes)
	{
		_msgQueue.Enqueue (bytes);
	}

	public void OnConnect()
	{
		isOnConnect = true;
	}

	void Update()
	{
		if (isOnConnect) {
			isOnConnect = false;
			if (_onConnect != null)
				_onConnect ();
		}
		int count = _msgQueue.Count;
		if (count > 0 && _onSocketMsg != null) {
			for (int i = 0; i < count; ++i) {
				_onSocketMsg (_msgQueue.Dequeue ());
			}
		}
	}
}
