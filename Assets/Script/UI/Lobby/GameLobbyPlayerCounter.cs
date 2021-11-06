using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class GameLobbyPlayerCounter : NetworkBehaviour
{
    [SyncVar]
    private int minPlayers;
    [SyncVar]
    private int maxPlayers;

    [SerializeField] private TMP_Text playerCount;

    public void UpdatePlayerCount()
    {
        var players = FindObjectsOfType<PlayerRoom>();
        bool isStartable = players.Length >= minPlayers;
        playerCount.text = players.Length + "/" + maxPlayers;
    }

    private void Start()
    {
        if (isServer)
        {
            var manager = NetworkManager.singleton as NetworkManagerLobby;
            minPlayers = manager.minPlayers;
            maxPlayers = manager.maxConnections;
        }
    }
}
