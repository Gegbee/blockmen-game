using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
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
        if (base.IsClient && GameManager.Instance.Host == null)
        {
            Debug.Log("host should be changing to: " + InstanceFinder.ClientManager.Connection.ClientId.ToString());
            GameManager.Instance.SetHost(InstanceFinder.ClientManager.Connection);
        }
        //if (base.IsOwner) username = ServerConnector.username;

    }
    public override void OnStopClient()
    {
        base.OnStopClient();
        if (base.IsClient && GameManager.Instance.Host == InstanceFinder.ClientManager.Connection)
        {
            GameManager.Instance.RemoveHost();
        }
    }
    public override void OnStartServer()
    {
        base.OnStartServer();
        username = OwnerId.ToString();
        //username = ServerConnector.username;
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isReady = !isReady;
        }
    }

}
