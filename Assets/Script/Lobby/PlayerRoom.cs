using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class PlayerRoom : NetworkRoomPlayer
{
    [SyncVar]
    public string nickname;

    public PlayerMovement lobbyPlayerMovement;

    private void Start()
    {
        base.Start();
        if (isServer)
        {
            SpawnLobbyPlayerCharacter();
            LobbyUI.instance.ActiveStartButton();
        }

        if (isLocalPlayer)
        {
            CmdSetNickname(PlayerSettings.nickname);
        }

        LobbyUI.instance.GameLobbyPlayerCounter.UpdatePlayerCount();
    }

    private void SpawnLobbyPlayerCharacter()
    {
        Vector3 spawnPos = FindObjectOfType<SpawnPosition>().GetSpawnPosition();

        var player = Instantiate(NetworkManagerLobby.singleton.spawnPrefabs[0], spawnPos, Quaternion.identity).GetComponent<LobbyPlayerMovement>();
        NetworkServer.Spawn(player.gameObject, connectionToClient);
        player.ownerNetId = netId;
    }

    [Command]
    public void CmdSetNickname(string nick)
    {
        nickname = nick;
        lobbyPlayerMovement.nickname = nick;
    }

    private void OnDestroy()
    {
        if(LobbyUI.instance != null)
        {
            LobbyUI.instance.GameLobbyPlayerCounter.UpdatePlayerCount();
        }
    }
}