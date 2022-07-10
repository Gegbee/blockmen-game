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
    public GameObject startButton;

    public override void OnStartClient()
    {
        base.OnStartClient();
        GameManager.OnPlayersUpdate += GameManager_OnPlayersUpdate;
        usernamesDisplay.text = GetUsernamesFormatted();
        addressDisplay.text = InstanceFinder.NetworkManager.GetComponent<Tugboat>().GetClientAddress();
    }
    public override void OnStopClient()
    {
        base.OnStopClient();
        GameManager.OnPlayersUpdate -= GameManager_OnPlayersUpdate;
    }
    private void GameManager_OnPlayersUpdate()
    {
        usernamesDisplay.text = GetUsernamesFormatted();
    }
    private string GetUsernamesFormatted()
    {
        string rt_text = "";
        Debug.Log(GameManager.Instance.Players.Count.ToString());
        foreach (PlayerManager entry in GameManager.Instance.Players)
        {
            rt_text += entry.GetUsername() + System.Environment.NewLine;
        }
        return rt_text;
    }
}
