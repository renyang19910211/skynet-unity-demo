using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sproto;

public class WindowChat : MonoBehaviour
{
    public InputField inputChat;
    public Text Item;

    void Awake()
    {
        ClientNet.inst.AddListener(Protocol.chat_msg.Tag, OnChatEvent);
    }

    public void OnBtnSendClick()
    {
        SprotoType.chat_msg.request req = new SprotoType.chat_msg.request();
        req.msg = inputChat.text;
        ClientNet.inst.Send<Protocol.chat_msg>(req);
        inputChat.text = "";
    }

    private void OnChatEvent(SprotoTypeBase evt)
    {
        SprotoType.chat_msg.request res = evt as SprotoType.chat_msg.request;
        Text item = GameObject.Instantiate(Item, Item.transform.parent, false);
        item.text = res.msg;
        item.gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log("test start");
    }

}
