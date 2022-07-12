using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet;
using FishNet.Transporting.Tugboat;
using TMPro;

public class LobbyDisplay : NetworkBehaviour
{
    public TMP_Text usernamesDisplay;
    public TMP_Text addressDisplay;
    public TMP_Text HostDisplay;
    public GameObject startButton;

    public void Start()
    {
        startButton.SetActive(false);
        HostDisplay.text = "";
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        GameManager.OnPlayersUpdate += GameManager_OnPlayersUpdate;
        GameManager.OnHostUpdate += GameManager_OnHostUpdate;
        usernamesDisplay.text = GetUsernamesFormatted();
        addressDisplay.text = InstanceFinder.NetworkManager.GetComponent<Tugboat>().GetClientAddress();
    }
    public override void OnStopClient()
    {
        base.OnStopClient();
        GameManager.OnPlayersUpdate -= GameManager_OnPlayersUpdate;
        GameManager.OnHostUpdate -= GameManager_OnHostUpdate;
    }
    private void GameManager_OnPlayersUpdate()
    {
        usernamesDisplay.text = GetUsernamesFormatted();
    }
    private string GetUsernamesFormatted()
    {
        string rt_text = "";
        foreach (PlayerManager entry in GameManager.Instance.Players)
        {
            rt_text += entry.GetUsername() + System.Environment.NewLine;
        }
        return rt_text;
    }
    private void GameManager_OnHostUpdate()
    {
        startButton.SetActive(true);
        HostDisplay.text = "ur the mf host";
    }
    public void OnLeavePressed()
    {
        if (IsClient) InstanceFinder.ClientManager.StopConnection();
        else InstanceFinder.ServerManager.StopConnection(false);
    }
}
