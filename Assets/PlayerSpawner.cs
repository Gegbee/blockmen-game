using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;

public class PlayerSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    public List<Transform> SpawnPoints;
    public override void OnStartClient()
    {
        base.OnStartClient();
        PlayerSpawn();
    }

    [ServerRpc(RequireOwnership = false)]
    public void PlayerSpawn(NetworkConnection client = null)
    {
        GameObject go = Instantiate(playerPrefab);
        //GameObject ca = Instantiate(cameraPrefab);'
        go.transform.position = SpawnPoints[Random.Range(0, SpawnPoints.Count)].position;
        Spawn(go, client);
        //ca.GetComponent<FollowerCamera>().target = go.transform;
    }
}
