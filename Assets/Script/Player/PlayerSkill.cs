using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSkill : NetworkBehaviour
{
    [SerializeField] private float range = 100f;
    //Shoot
    [Command]
    public void Shoot()
    {
        RaycastHit hit; 
        if(Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
    }
    //Bomb
}
