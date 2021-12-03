using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManagerLobby : NetworkRoomManager
{
    public int policeCount;

    public override void OnRoomServerConnect(NetworkConnection conn)
    {
        base.OnRoomServerConnect(conn);
    }

    public override void OnStartClient()
    {
        Debug.Log("Whyy That Happen in OnStartClient??");
        base.OnStartClient();
    }

    //public override void OnRoomServerAddPlayer(NetworkConnection conn)
    //{
    //    if (IsSceneActive(GameplayScene))
    //    {
    //        Debug.Log("Masuk OnRoomServerAddPlayer in gameplay(?)");
    //    }
    //    
    //    SpawnPosition[] spawnPos = FindObjectsOfType<SpawnPosition>();
    //
    //    var roomPlayers = FindObjectsOfType<PlayerRoom>();
    //    foreach(var rp in roomPlayers)
    //    {
    //        if(rp.netId == conn.connectionId)
    //        {
    //            if(rp.playerType == PlayerType.police)
    //            {
    //                GameObject player = Instantiate(spawnPrefabs[1], spawnPos[1].GetSpawnPosition(), Quaternion.identity);
    //                player.name = $"{spawnPrefabs[1].name} [connId={conn.connectionId}]";
    //                NetworkServer.AddPlayerForConnection(conn, player);
    //                return;
    //            }
    //            else
    //            {
    //                GameObject player = Instantiate(spawnPrefabs[2], spawnPos[0].GetSpawnPosition(), Quaternion.identity);
    //                player.name = $"{spawnPrefabs[2].name} [connId={conn.connectionId}]";
    //                NetworkServer.AddPlayerForConnection(conn, player);
    //                return;
    //            }
    //        }
    //    }
    //}

    //public override GameObject OnRoomServerCreateGamePlayer(NetworkConnection conn, GameObject roomPlayer)
    //{
    //    Debug.Log("Masuk OnRoomServerCreateGamePlayer");
    //    var players = FindObjectsOfType<PlayerRoom>();
    //    Debug.Log("playerroom : " + players.Length);
    //    //SpawnPosition[] spawnPos = FindObjectsOfType<SpawnPosition>();
    //    //
    //    //var roomPlayers = FindObjectsOfType<PlayerRoom>();
    //    //foreach (var rp in roomPlayers)
    //    //{
    //    //    if (rp.netId == conn.connectionId)
    //    //    {
    //    //        if (rp.playerType == PlayerType.police)
    //    //        {
    //    //            GameObject player = Instantiate(spawnPrefabs[1], spawnPos[1].GetSpawnPosition(), Quaternion.identity);
    //    //            player.name = $"{spawnPrefabs[1].name} [connId={conn.connectionId}]";
    //    //            //NetworkServer.AddPlayerForConnection(conn, player);
    //    //            return player;
    //    //        }
    //    //        else
    //    //        {
    //    //            GameObject player = Instantiate(spawnPrefabs[2], spawnPos[0].GetSpawnPosition(), Quaternion.identity);
    //    //            player.name = $"{spawnPrefabs[2].name} [connId={conn.connectionId}]";
    //    //            //NetworkServer.AddPlayerForConnection(conn, player);
    //    //            return player;
    //    //        }
    //    //    }
    //    //}
    //    //return null;
    //    return base.OnRoomServerCreateGamePlayer(conn, roomPlayer);
    //}

    //public override void OnRoomServerSceneChanged(string sceneName)
    //{
    //    if(sceneName == GameplayScene)
    //    {
    //        Debug.Log("Masuk OnRoomServerSceneChanged");
    //        var players = FindObjectsOfType<PlayerRoom>();
    //        Debug.Log("playerroom : " + players.Length);
    //
    //        //SpawnPosition[] spawnPos = FindObjectsOfType<SpawnPosition>();
    //        //
    //        //var roomPlayers = FindObjectsOfType<PlayerRoom>();
    //        //foreach (var rp in roomPlayers)
    //        //{
    //        //    if (rp.netId == conn.connectionId)
    //        //    {
    //        //        if (rp.playerType == PlayerType.police)
    //        //        {
    //        //            GameObject player = Instantiate(spawnPrefabs[1], spawnPos[1].GetSpawnPosition(), Quaternion.identity);
    //        //            player.name = $"{spawnPrefabs[1].name} [connId={conn.connectionId}]";
    //        //            //NetworkServer.AddPlayerForConnection(conn, player);
    //        //            return player;
    //        //        }
    //        //        else
    //        //        {
    //        //            GameObject player = Instantiate(spawnPrefabs[2], spawnPos[0].GetSpawnPosition(), Quaternion.identity);
    //        //            player.name = $"{spawnPrefabs[2].name} [connId={conn.connectionId}]";
    //        //            //NetworkServer.AddPlayerForConnection(conn, player);
    //        //            return player;
    //        //        }
    //        //    }
    //        //}
    //        //return null;
    //    }
    //
    //    base.OnRoomServerSceneChanged(sceneName);
    //}

    //public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnection conn, GameObject roomPlayer, GameObject gamePlayer)
    //{
    //    Debug.Log("Masuk OnRoomServerSceneLoadedForPlayer");
    //    var players = FindObjectsOfType<PlayerRoom>();
    //    Debug.Log("playerroom : " + players.Length);
    //    //if(gamePlayer.GetComponent<LobbyPlayerMovement>() == null)
    //    //{
    //    //    gamePlayer.AddComponent<LobbyPlayerMovement>();
    //    //    return true;
    //    //}
    //    return true;
    //}
}
