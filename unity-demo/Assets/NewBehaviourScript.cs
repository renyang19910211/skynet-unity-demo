using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;
using Sproto;

public class NewBehaviourScript : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
        ClientNet.inst.Connect("127.0.0.1", 8899, OnConnect);
    }

	private void OnConnect(Sproto.SprotoTypeBase _)
	{
        Debug.Log("on connect");
        ClientNet.inst.Send<Protocol.handshake>(null, evt =>
        {
            SprotoType.handshake.response response = evt as SprotoType.handshake.response;
            Debug.Log("on msg " + response.msg);
        });

        SprotoType.register_account.request req = new SprotoType.register_account.request();
        req.account = "test account";
        req.password = "test password";
        ClientNet.inst.Send<Protocol.register_account>(req, evt =>
        {
            SprotoType.register_account.response response = evt as SprotoType.register_account.response;
            Debug.Log("on msg " + response.msg);


        });

        //SprotoType.login_account.request loginReq = new SprotoType.login_account.request();
        //loginReq.account = "test account";
        //loginReq.password = "test password";
        //ClientNet.inst.Send<Protocol.login_account>(loginReq, e =>
        //{
        //    SprotoType.login_account.response res = e as SprotoType.login_account.response;
        //    Debug.Log("on login " + res.result);
        //});
    }
}
