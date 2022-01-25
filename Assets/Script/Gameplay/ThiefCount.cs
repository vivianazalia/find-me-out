using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class ThiefCount : NetworkBehaviour
{
    public static ThiefCount instance = null;

    [SyncVar(hook = nameof(UpdateThiefCountText_Hook))]
    public int thiefCount;

    [Header("UI")]
    [SerializeField] private TMP_Text thiefCountText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        var manager = NetworkManager.singleton as NetworkManagerLobby;
        int policeCount = manager.policeCount;
        int maxConn = manager.maxConnections;
        thiefCount = maxConn - policeCount;
    }

    public void UpdateThiefCountText_Hook(int oldValue, int newValue)
    {
        thiefCountText.text = "Thief Remains : " + newValue;
    }

    public void UpdateThiefCount()
    {
        thiefCount = 0;

        var players = FindObjectsOfType<InGameCharacterPlayer>();

        foreach(var p in players)
        {
            if(p.playerType == PlayerType.thief)
            {
                thiefCount += 1; 
            }
        }

        //RpcUpdateTextThiefCount();
        thiefCountText.text = "Thief Remains : " + thiefCount.ToString();
    }

    public void UpdateTextThiefCount()
    {
        thiefCountText.text = "Thief Remains : " + thiefCount.ToString();
    }
}
