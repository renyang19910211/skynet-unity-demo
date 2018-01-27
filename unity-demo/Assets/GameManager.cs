using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{

    void Start()
    {
        ClientNet.inst.Connect("127.0.0.1", 8899, OnConnect);
    }

    private void OnConnect(Sproto.SprotoTypeBase _)
    {
        Debug.Log("on connect");
    }
}
