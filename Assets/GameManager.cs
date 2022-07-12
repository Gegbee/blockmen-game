using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet;
using FishNet.Transporting;
using FishNet.Object.Synchronizing;
using FishNet.Connection;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }

    public static event System.Action OnPlayersUpdate;
    public static event System.Action OnHostUpdate;

    [SyncObject]
    public readonly SyncList<PlayerManager> Players = new();

    [field: SyncVar(Channel = Channel.Reliable, OnChange = nameof(OnHost))]
    public NetworkConnection Host { get; set; }

    private void Awake()
    {
        Instance = this;
        Players.OnChange += Players_OnChange;
    }


    private void OnHost(NetworkConnection prev, NetworkConnection next, bool asServer)
    {
        if (asServer) return;
        if (next == null)
        {
            InstanceFinder.ClientManager.StopConnection();
            // Go back to FFA menu scene
        }
        if (Host.IsLocalClient)
        {
            OnHostUpdate?.Invoke();
        }
    }

    private void Players_OnChange(SyncListOperation op, int index, PlayerManager oldItem, PlayerManager newItem, bool asServer)
    {
        OnPlayersUpdate?.Invoke();
        switch (op)
        {
            case SyncListOperation.Add:
                break;
            case SyncListOperation.RemoveAt:
                break;
            case SyncListOperation.Insert:
                break;
            case SyncListOperation.Set:
                break;
            case SyncListOperation.Clear:
                break;
            case SyncListOperation.Complete:
                break;
        }
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
    [Client]
    public void RemoveHost()
    {
        ServerRemoveHost();
    }
    [ServerRpc(RequireOwnership = false)]
    public void ServerRemoveHost()
    {
        Host = null;

    }
}
