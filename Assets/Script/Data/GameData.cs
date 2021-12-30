using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public struct GameRuleData : NetworkMessage
{
    public float bomCooldown;
    public float shootCooldown;
    public int bulletCount;
    public float gameplayDuration;
    public float hidingTime;
}

public class GameData : NetworkBehaviour
{
    [SyncVar]
    private float bomCooldown;
    [SyncVar]
    private float shootCooldown;
    [SyncVar]
    private int bulletCount;
    [SyncVar]
    private float gameplayDuration;
    [SyncVar]
    private float hidingTime;

    private void SetGameRuleData()
    {
        bomCooldown = 5f;
        shootCooldown = .5f;
        bulletCount = 100;
        gameplayDuration = 120f;
        hidingTime = 10f;
    }

    private void Start()
    {
        if (isServer)
        {
            SetGameRuleData();
        }
    }

    public GameRuleData GetGameRuleData()
    {
        return new GameRuleData
        {
            bomCooldown = bomCooldown,
            shootCooldown = shootCooldown,
            bulletCount = bulletCount,
            gameplayDuration = gameplayDuration,
            hidingTime = hidingTime
        };
    }
}
