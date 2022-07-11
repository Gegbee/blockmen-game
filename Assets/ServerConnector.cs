using System.Collections;
using System.Collections.Generic;
using FishNet;
using UnityEngine;
using TMPro;

public class ServerConnector : MonoBehaviour
{
    public TMP_InputField UsernameInput;
    public static string username = "";
    public void OnJoinPressed()
    {
        if (!InputCheck()) return;
        Debug.Log(username);
        username = UsernameInput.text;
        InstanceFinder.ClientManager.StartConnection();
    }
    public void OnHostPressed()
    {
        if (!InputCheck()) return;
        username = UsernameInput.text;
        InstanceFinder.ServerManager.StartConnection();
    }
    private bool InputCheck()
    {
      if (UsernameInput.text.Length == 0) return false;
        return true;
    }
}
