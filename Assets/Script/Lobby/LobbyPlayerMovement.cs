using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayerMovement : MyPlayer
{
    [SyncVar (hook = nameof(SetOwnerNetId_Hook))]
    public uint ownerNetId;

    public void SetOwnerNetId_Hook(uint oldValue, uint newValue)
    {
        var players = FindObjectsOfType<PlayerRoom>();
        foreach(var player in players)
        {
            if(ownerNetId == player.netId)
            {
                player.lobbyPlayerMovement = this;
                break;
            }
        }
    }
}
