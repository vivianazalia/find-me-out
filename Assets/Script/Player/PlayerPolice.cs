using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerPolice : NetworkBehaviour
{
    public Transform bulletPos;

    [SerializeField] private float range = 100f;

    [Command]
    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(bulletPos.position, bulletPos.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            PlayerThief thief = hit.transform.GetComponent<PlayerThief>();
            if (thief != null)
            {
                thief.CmdTakeDamage(50);
            }
        }
    }
}
