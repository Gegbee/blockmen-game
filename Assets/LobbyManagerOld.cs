using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Connection;
using FishNet.Transporting.Tugboat;
using FishNet.Object;
using TMPro;

public class LobbyManagerOld : NetworkBehaviour
{
    public TMP_Text usernamesDisplay;
    public TMP_Text addressDisplay;
    public GameObject startButton;
    public override void OnStartClient()
    {
        base.OnStartClient();
        
        Debug.Log("Current Playuers: " + GameManagerOld.Instance.Players.Count.ToString());
        if (GameManagerOld.Instance.Players.Count == 0)
        {
            Debug.Log("setting host should work");
            GameManagerOld.Instance.SetHost(Owner);
        }
        GameManagerOld.Instance.SetUsername(PlayerData.instance.username);
        GameManagerOld.OnPlayersUpdate += GameManagerOld_OnPlayersUpdate;
        GameManagerOld.OnHostUpdate += GameManagerOld_OnHostUpdate;
    }
    public override void OnStopClient()
    {
        base.OnStopClient();
        GameManagerOld.OnPlayersUpdate -= GameManagerOld_OnPlayersUpdate;

    }
    private void GameManagerOld_OnPlayersUpdate(NetworkConnection nc, string username)
    {
        usernamesDisplay.text = GameManagerOld.Instance.GetUsernamesFormatted();
    }
    private void GameManagerOld_OnHostUpdate()
    {
        addressDisplay.enabled = true;
        startButton.SetActive(true);
        addressDisplay.text = InstanceFinder.NetworkManager.GetComponent<Tugboat>().GetClientAddress();
    }
}
