using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class InGamePlayerMovement : PlayerMovement
{
    private float health = 100;
    public Transform bulletPos;
    [SerializeField] private float range = 100f;

    [SyncVar(hook = nameof(SetPlayerType_Hook))]
    public PlayerType playerType;

    public void SetPlayerType_Hook(PlayerType oldValue, PlayerType newValue)
    {
        playerType = newValue;
        var manager = NetworkManager.singleton as NetworkManagerLobby;
        if (hasAuthority)
        {
            if (newValue == PlayerType.police)
            {
                InGameUIManager.instance.shootButton.Show(this);
            } 
        }
    }

    protected override void Start()
    {
        base.Start();
        GameManager.instance.AddPlayer(this);
    }

    [Command]
    public void CmdShoot(InGamePlayerMovement target)
    {
        Debug.Log("Masuk cmdshoot");
        if (target != null && target.playerType == PlayerType.thief)
        {
            target.health -= 50;
            TakeDamage(target.connectionToClient, target.health);
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(bulletPos.position, bulletPos.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            if(hit.transform.GetComponent<NetworkIdentity>() == null)
            {
                StartCoroutine(SetAnimAttackDelay(.3f));
                return;
            }
            else
            {
                InGamePlayerMovement target = hit.transform.gameObject.GetComponent<InGamePlayerMovement>();
                if (target != null)
                {
                    CmdShoot(target);
                }
            }
        }

        StartCoroutine(SetAnimAttackDelay(.3f));
    }

    [TargetRpc]
    public void TakeDamage(NetworkConnection conn, float currHealth)
    {
        Debug.Log(conn.identity.gameObject.name + "'s Health is " + currHealth);
    }

    private IEnumerator SetAnimAttackDelay(float delay)
    {
        anim.SetBool("isAttack", true);
        yield return new WaitForSeconds(delay);
        anim.SetBool("isAttack", false);
    }
}
