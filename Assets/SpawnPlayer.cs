using FishNet.Connection;
using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : NetworkBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    //[SerializeField] private GameObject cameraPrefab;
    public override void OnStartClient()
    {
        base.OnStartClient();
        PlayerSpawn();
    }

    [ServerRpc(RequireOwnership = false)]
    public void PlayerSpawn(NetworkConnection client = null) {
        GameObject go = Instantiate(playerPrefab);
        //GameObject ca = Instantiate(cameraPrefab);
        Spawn(go, client);
        //ca.GetComponent<FollowerCamera>().target = go.transform;
    }
}
