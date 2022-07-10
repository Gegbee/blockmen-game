using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class PlayerManager : NetworkBehaviour
{
    [SyncVar]
    public string username;

    [SyncVar]
    public bool isReady;

    public override void OnStartClient()
    {
        base.OnStartClient();
        
    }
    public override void OnStartServer()
    {
        base.OnStartServer();
        GameManager.Instance.Players.Add(this);
    }
    public override void OnStopServer()
    {
        base.OnStopServer();
        GameManager.Instance.Players.Remove(this);
    }
    public string GetUsername()
    {
        return username;
    }
}
