using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayerMovement : MyPlayer
{
    [SyncVar (hook = nameof(SetOwnerNetId_Hook))]
    public uint ownerNetId;

    public override void Start()
    {
        base.Start();
        if (hasAuthority)
        {
            CmdSetPlayerCharacter(GameSetting.nickname);
        }
    }

    [Command]
    private void CmdSetPlayerCharacter(string nickname)
    {
        this.nickname = nickname;
        ShowNickname(this.nickname);
    }

    [ClientRpc]
    public void ShowNickname(string nick)
    {
        nicknameText.text = nick;
    }

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
