using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet;
using FishNet.Object.Synchronizing;
using FishNet.Connection;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }

    public static event System.Action OnPlayersUpdate;

    [SyncObject]
    public readonly SyncList<PlayerManager> Players = new();

    private void Awake()
    {
        Instance = this;
        Players.OnChange += Players_OnChange;
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
}
