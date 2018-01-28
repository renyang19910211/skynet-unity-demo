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
        ClientNet.inst.AddListener(Protocol.chat_msgs.Tag, OnPushChat);
    }

    public void OnBtnSendClick()
    {
        if (string.IsNullOrEmpty(inputChat.text)) return;
        SprotoType.chat_msg.request req = new SprotoType.chat_msg.request();
        req.msg = inputChat.text;
        ClientNet.inst.Send<Protocol.chat_msg>(req);
        inputChat.text = "";
    }

    private void OnPushChat(SprotoTypeBase evt)
    {
        SprotoType.chat_msgs.request res = evt as SprotoType.chat_msgs.request;
        foreach(string each in res.msgs)
        {
            Text item = GameObject.Instantiate(Item, Item.transform.parent, false);
            item.text = each;
            item.gameObject.SetActive(true);
        }
    }


    private void OnChatEvent(SprotoTypeBase evt)
    {
        SprotoType.chat_msg.request res = evt as SprotoType.chat_msg.request;
        Text item = GameObject.Instantiate(Item, Item.transform.parent, false);
        item.text = res.msg;
        item.gameObject.SetActive(true);
    }
}
