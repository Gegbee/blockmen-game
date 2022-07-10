using FishNet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientServerManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool isServer;
    void Awake() {
      if (isServer) InstanceFinder.ServerManager.StartConnection();
      else InstanceFinder.ClientManager.StartConnection();
    }
}
