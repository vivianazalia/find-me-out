using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManagerLobby : NetworkRoomManager
{
    [SerializeField] private Canvas canvasPanelUILobby = null;

    private void Update()
    {
        if (IsSceneActive(RoomScene))
        {
            canvasPanelUILobby = FindObjectOfType<Canvas>();
        }
    }

    public override void OnRoomServerConnect(NetworkConnection conn)
    {
        base.OnRoomServerConnect(conn);

        if (IsSceneActive(RoomScene))
        {
            SpawnPosition spawnPos = FindObjectOfType<SpawnPosition>();

            if (canvasPanelUILobby != null)
            {
                var player = Instantiate(spawnPrefabs[0], spawnPos.GetSpawnPosition(), Quaternion.identity);
                player.transform.SetParent(canvasPanelUILobby.transform);
                NetworkServer.Spawn(player, conn);
            }
        }
    }


}
