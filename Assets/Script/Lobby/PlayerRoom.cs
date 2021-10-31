using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class PlayerRoom : NetworkRoomPlayer
{
    [SerializeField] private PanelPlayer panelPlayer;

    public void DrawPlayerPanel()
    {
        Canvas canvas = FindObjectOfType<Canvas>();

        Vector3 spawnPos = FindObjectOfType<SpawnPosition>().GetSpawnPosition();

        GameObject player = Instantiate(panelPlayer.gameObject, spawnPos, Quaternion.identity);
        player.transform.position = spawnPos;
        player.transform.SetParent(canvas.transform);
    }

    public override void OnClientEnterRoom()
    {
        base.OnClientEnterRoom();

        //if (isLocalPlayer)
        //{
        //    Debug.Log("On Client Enter Room");
        //
        //    NetworkRoomManager room = NetworkManager.singleton as NetworkRoomManager;
        //    if (room)
        //    {
        //        if (!NetworkManager.IsSceneActive(room.RoomScene)) return;
        //
        //        DrawPlayerPanel();
        //    }
        //}
    }

    public void ReadyButton()
    {
        if (readyToBegin)
        {
            CmdChangeReadyState(true);
        }
        else
        {
            CmdChangeReadyState(false);
        }
    }
}
