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

    [SerializeField] private TMP_Text playerCountText;

    public void UpdatePlayerCount()
    {
        var players = FindObjectsOfType<PlayerRoom>();
        bool isStartable = players.Length >= minPlayers;
        playerCountText.color = isStartable ? Color.white : Color.red;
        playerCountText.text = players.Length + "/" + maxPlayers;
        //LobbyUI.instance.SetInteractableStartButton(isStartable);

        StartCoroutine(StartGame(isStartable));
    }

    public IEnumerator StartGame(bool canStart)
    {
        yield return new WaitForSeconds(1f);
        if (canStart)
        {
            LobbyUI.instance.OnClickStartButton();
        }
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
