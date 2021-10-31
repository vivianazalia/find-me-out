using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManagerLobby : NetworkRoomManager
{
    public override void OnRoomServerConnect(NetworkConnection conn)
    {
        base.OnRoomServerConnect(conn);

        Vector3 spawnPos = FindObjectOfType<SpawnPosition>().GetSpawnPosition();

        var player = Instantiate(spawnPrefabs[0], spawnPos, Quaternion.identity);
        NetworkServer.Spawn(player, conn);

        //if (IsSceneActive(RoomScene))
        //{
        //    SpawnPosition spawnPos = FindObjectOfType<SpawnPosition>();
        //
        //    if (canvasPanelUILobby != null)
        //    {
        //        var player = Instantiate(spawnPrefabs[0], spawnPos.GetSpawnPosition(), Quaternion.identity);
        //        player.transform.SetParent(canvasPanelUILobby.transform);
        //        NetworkServer.Spawn(player, conn);
        //    }
        //}
    }


}
