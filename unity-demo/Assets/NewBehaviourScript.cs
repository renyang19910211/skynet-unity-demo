using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;
using Sproto;

public class NewBehaviourScript : MonoBehaviour 
{
	private NetTransporter _client;
	private SprotoRpc _sprotoRpc;
	private SprotoRpc.RpcRequest _host;

	// Use this for initialization
	void Start () 
	{
        ClientNet.inst.Connect("127.0.0.1", 8888, OnConnect);
	}

	private void OnConnect(Sproto.SprotoTypeBase _)
	{
		Debug.Log ("on connect");
		ClientNet.inst.Send<Protocol.handshake>(null, evt => {
			SprotoType.handshake.response response = evt as SprotoType.handshake.response;
			Debug.Log("on msg " + response.msg);
		});

		SprotoType.register_account.request req = new SprotoType.register_account.request ();
		req.account = "test account";
		req.password = "password";
		ClientNet.inst.Send<Protocol.register_account>(req, evt => {
			SprotoType.register_account.response response = evt as SprotoType.register_account.response;
			Debug.Log("on msg " + response.msg);
		});
	}
}
