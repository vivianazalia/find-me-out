using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerThief : NetworkBehaviour
{
    [SerializeField] private int health = 100;

    [Command]
    public void CmdTakeDamage(int damage)
    {
        health -= damage;
    }

    public int GetCurrentHealth()
    {
        return health;
    }
}
