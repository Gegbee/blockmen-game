using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using FishNet;
using FishNet.Object.Synchronizing;
using FishNet.Connection;
using UnityEngine;

public sealed class GameManagerOld : NetworkBehaviour
{
    public static GameManagerOld Instance { get; private set; }

    public static event System.Action<NetworkConnection, string> OnPlayersUpdate;
    public static event System.Action OnHostUpdate;

    [field: SyncObject]
    public SyncDictionary<NetworkConnection, string> Players { get; } = new SyncDictionary<NetworkConnection, string>();

    [field: SyncVar(OnChange = nameof(OnHost))]
    public NetworkConnection Host { get; set; }

    [field: SyncVar]
    public bool CanStart { get; private set; }

    [field: SyncVar]
    public bool DidStart { get; private set; }

    public void OnHost(NetworkConnection prev, NetworkConnection next, bool asServer)
    {
        Debug.Log("CHANGED AND RECIEVED");
        Debug.Log(Host.ClientId.ToString());
        Debug.Log(Owner.ClientId.ToString());
        if (Owner.Equals(Host))
        {
            OnHostUpdate?.Invoke();
            Debug.Log("OWNER IS NOW HOST");
        }
    }
    public string GetUsernamesFormatted()
    {
        string rt_text = "";
        foreach (KeyValuePair<NetworkConnection, string> entry in Players)
        {
            rt_text += entry.Value + System.Environment.NewLine;
        }
        return rt_text;
    }
    private void Awake()
    {
        Instance = this;
        Players.OnChange += Players_OnChange;
    }

    private void Players_OnChange(SyncDictionaryOperation op,
    NetworkConnection key, string value, bool asServer)
    {
        switch (op)
        {
            //Adds key with value.
            case SyncDictionaryOperation.Add:
                OnPlayersUpdate?.Invoke(key, value);
                break;
            //Removes key.
            case SyncDictionaryOperation.Remove:
                OnPlayersUpdate?.Invoke(key, value);
                break;
            //Sets key to a new value.
            case SyncDictionaryOperation.Set:
                OnPlayersUpdate?.Invoke(key, value);
                break;
            //Clears the dictionary.
            case SyncDictionaryOperation.Clear:
                break;
            //Like SyncList, indicates all operations are complete.
            case SyncDictionaryOperation.Complete:
                break;
        }
    }
    [Client]
    public void SetUsername(string name)
    {
        ServerSetUsername(name);
    }

    [ServerRpc(RequireOwnership = false)]
    public void ServerSetUsername(string name, NetworkConnection sender = null)
    {
        Players[sender] = name;
        // right here update the TMPro in the lobby manager
    }

    [Client]
    public void RemoveUsername()
    {
        ServerRemoveUsername();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ServerRemoveUsername(NetworkConnection sender = null)
    {
        Players.Remove(sender);
    }


    [Client]
    public void SetHost(NetworkConnection host)
    {
        Debug.Log("attempting to set host");
        ServerSetHost(host);
    }
    [ServerRpc(RequireOwnership = false)]
    public void ServerSetHost(NetworkConnection host)
    {
        if (Host != null) return;
        Debug.Log(host.ToString());
        Host = host;
        
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkManager.ServerManager.OnRemoteConnectionState += ServerManager_OnRemoteConnectionState;
    }
    public override void OnStopServer()
    {
        base.OnStopServer();
        NetworkManager.ServerManager.OnRemoteConnectionState -= ServerManager_OnRemoteConnectionState;
    }
    private void ServerManager_OnRemoteConnectionState(NetworkConnection arg1, FishNet.Transporting.RemoteConnectionStateArgs arg2)
    {
        if (arg2.ConnectionState != FishNet.Transporting.RemoteConnectionState.Started)
        {
            Players.Remove(arg1);
            Debug.Log(Players);
            Debug.Log(GetUsernamesFormatted());
        }
    }
}
