using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySettings : MonoBehaviour
{
    public void LeaveRoom()
    {
        var manager = NetworkManagerLobby.singleton;

        if(manager.mode == Mirror.NetworkManagerMode.Host)
        {
            manager.StopHost();
        }
        else if(manager.mode == Mirror.NetworkManagerMode.ClientOnly)
        {
            manager.StopClient();
        }
    }
}
