using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sproto;

public class WindowLogin : MonoBehaviour
{
    public InputField account;
    public InputField password;


	public void OnBtnLoginClick()
    {
        SprotoType.login_account.request loginReq = new SprotoType.login_account.request();
        loginReq.account = account.text;
        loginReq.password = password.text;
        ClientNet.inst.Send<Protocol.login_account>(loginReq, e =>
        {
            SprotoType.login_account.response res = e as SprotoType.login_account.response;
            Debug.Log("on login " + res.result);
        });
    }

    public void OnBtnRegisterClick()
    {
        SprotoType.register_account.request req = new SprotoType.register_account.request();
        req.account = account.text;
        req.password = password.text;
        ClientNet.inst.Send<Protocol.register_account>(req, evt =>
        {
            SprotoType.register_account.response response = evt as SprotoType.register_account.response;
            Debug.Log("on register " + response.msg);
        });
    }
}
