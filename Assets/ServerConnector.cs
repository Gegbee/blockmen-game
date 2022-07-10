using System.Collections;
using System.Collections.Generic;
using FishNet;
using UnityEngine;
using TMPro;

public class ServerConnector : MonoBehaviour
{
    public void OnJoinPressed()
    {
        InstanceFinder.ClientManager.StartConnection();
    }
    public void OnHostPressed()
    {
        InstanceFinder.ServerManager.StartConnection();
    }
    //private bool InputCheck()
    //{
    //  if (usernameTextEdit.text.Length == 0) return false;
    //    return true;
    //}
}
