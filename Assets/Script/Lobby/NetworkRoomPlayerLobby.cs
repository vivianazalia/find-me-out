using Mirror;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NetworkRoomPlayerLobby : NetworkBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text[] playerNameText = new TMP_Text[4];
    [SerializeField] private TMP_Text[] playerReadyText = new TMP_Text[4];
    [SerializeField] private Button startButton;

    [SyncVar(hook = "HandleDisplayNameChanged")]
    public string displayName = "Loading...";
    [SyncVar(hook = "HandleReadyStateChanged")]
    public bool isReady = false;

    private bool isLeader;
    public bool IsLeader
    {
        set
        {
            isLeader = value;
            startButton.gameObject.SetActive(value);
        }
    }

    private NetworkManagerLobby room;
    public NetworkManagerLobby Room
    {
        get
        {
            if(room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerLobby;
        }
    }

    public override void OnStartAuthority()
    {
        CmdSetDisplayName(PlayerInputName.DisplayName);
    }

    public override void OnStartClient()
    {
        Room.RoomPlayers.Add(this);
    }

    //OnNetworkDestroy tidak ada 

    public void HandleReadyStateChanged(bool oldValue, bool newValue) => UpdateDisplay();

    public void HandleDisplayNameChanged(string oldValue, string newValue) => UpdateDisplay();

    private void UpdateDisplay()
    {
        if (!hasAuthority)
        {
            foreach(var player in Room.RoomPlayers)
            {
                if (player.hasAuthority)
                {
                    player.UpdateDisplay();
                    break;
                }
            }

            return;
        }

        for(int i = 0; i < playerNameText.Length; i++)
        {
            playerNameText[i].text = "Waiting for player...";
            playerReadyText[i].text = string.Empty;
        }

        for(int i = 0; i < Room.RoomPlayers.Count; i++)
        {
            playerNameText[i].text = Room.RoomPlayers[i].displayName;
            playerReadyText[i].text = Room.RoomPlayers[i].isReady ?
                "<color=green>Ready</color>" : "<color=red>Not Ready</color>";
        }
    }

    public void HandleReadyToStart(bool readyToStart)
    {
        if (!isLeader) { return; }

        startButton.interactable = readyToStart;
    }

    [Command]
    private void CmdSetDisplayName(string dn)
    {
        displayName = dn;
    }

    [Command]
    public void CmdReadyUp()
    {
        isReady = !isReady;
        Room.NotifyPlayerForReadyState();
    }

    [Command]
    public void CmdStartGame()
    {
        if(Room.RoomPlayers[0].connectionToClient != connectionToClient) { return; }

        //start game
    }

}
