using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public struct PlayerThiefMessage : NetworkMessage
{
    public string nickname;
    public float bombCooldown;
    public float health;
}

public enum PlayerType
{
    participant,
    thief,
    police,
    viewer
}

public enum WinLoseState
{
    player,
    winner,
    loser
}

public class InGameCharacterPlayer : MyPlayer
{
    [SerializeField] private PlayerFinder playerFinder = null;
    [SerializeField] private ObjectFinder objectFinder = null;

    [SyncVar]
    private float bomCooldown;
    public float BomCooldown { get { return bomCooldown; } }
    [SyncVar]
    private float shootCooldown;
    public float ShootCooldown { get { return shootCooldown; } }
    [SyncVar]
    private int bulletCount;
    public int BulletCount { get { return bulletCount; } }
    [SyncVar]
    public float health;
    public float Health { get { return health; } }

    [SyncVar(hook = nameof(SetIsLose_Hook))]
    public WinLoseState state;

    [SyncVar]
    public PlayerType playerType;

    public bool IsShootable { get { return shootCooldown < 0; } }
    public bool CanThrowBom { get { return bomCooldown < 0; } }

    public void SetIsLose_Hook(WinLoseState oldValue, WinLoseState newValue)
    {
        if (newValue == WinLoseState.winner)
        {
            ShowPanelWin(this.connectionToClient);
        }
        else if (newValue == WinLoseState.loser)
        {
            ShowPanelLose(this.connectionToClient);
        }
    }

    public override void Start()
    {
        base.Start();
        if (hasAuthority)
        {
            var myRoomPlayer = PlayerRoom.instance;
            //SetCameraTransform(gameObject);
            CmdSetPlayerCharacter(myRoomPlayer.nickname);
            SetUI();
        }

        GameManager.Instance.AddPlayer(this);

        state = WinLoseState.player;

        if (playerType == PlayerType.thief)
        {
            GameManager.Instance.AddToThiefList(this);
        }
        //Debug.Log("Health start : " + health);
    }

    [Command(requiresAuthority = false)]
    public void CmdSetType(PlayerType type)
    {
        //Debug.Log("Playertype sblm : " + playerType + " CmdSetType NetId: " + netId + " CmdSetType " + type);
        playerType = type;
    }

    public void SetType(PlayerType type)
    {
        CmdSetType(type);
    }

    private void SetUI()
    {
        if (playerType == PlayerType.police)
        {
            InGameUIManager.instance.ShootButton.Show(this);
            InGameUIManager.instance.ShootButton.Interactable(false);
        }
        else if (playerType == PlayerType.thief)
        {
            InGameUIManager.instance.BomButton.Show(this);
            InGameUIManager.instance.HealthBar.Show(this);
        }
    }

    public void SetInteractableUI(bool b)
    {
        if (playerType == PlayerType.police)
        {
            InGameUIManager.instance.ShootButton.Interactable(b);
        }
    }

    public void SetCameraTransform(GameObject parent)
    {
        Camera cam = Camera.main;
        cam.transform.SetParent(parent.transform);
        cam.transform.localPosition = new Vector3(0, 0, cam.transform.position.z);
    }

    public void SetSkillCooldown()
    {
        if (isServer)
        {
            bomCooldown = GameManager.Instance.bomCooldown;
            shootCooldown = GameManager.Instance.shootCooldown;
            bulletCount = GameManager.Instance.bulletCount;
            health = GameManager.Instance.healthAmount;
        }
    }

    [ClientRpc]
    public void RpcPosition(Vector3 position)
    {
        transform.position = position;
    }

    [Command]
    private void CmdSetPlayerCharacter(string nickname)
    {
        this.nickname = nickname;
    }

    public void Update()
    {
        if (!hasAuthority) return;

        CooldownSkill();
    }

    private void CooldownSkill()
    {
        if (playerType == PlayerType.police)
        {
            shootCooldown -= Time.deltaTime;
        }

        if (playerType == PlayerType.thief)
        {
            bomCooldown -= Time.deltaTime;
        }
    }

    #region Shoot
    [Command]
    private void CmdShoot(uint targetNetId)
    {
        InGameCharacterPlayer target = null;
        foreach (var player in GameManager.Instance.GetPlayerList())
        {
            if (player.netId == targetNetId)
            {
                target = player;
            }
        }

        if (target != null)
        {
            if (target.health > 0)
            {
                target.health -= .2f;
                TakeDamage(target.connectionToClient, target.health);
            }
            else
            {
                //ghost mode
                target.CmdDead(target);
                //masuk ke list thief lose
                //server cek thief player, if no thief gamestate = over
            }
        }
        //shootCooldown = GameSystem.Instance.shootCooldown;
    }

    public void Shoot()
    {
        StartCoroutine(IsAttack(.2f));
        if (playerFinder)
        {
            if (playerFinder.GetFirstTarget() != null)
            {
                CmdShoot(playerFinder.GetFirstTarget().netId);
            }
        }
        shootCooldown = GameManager.Instance.shootCooldown;
    }

    [TargetRpc]
    public void TakeDamage(NetworkConnection conn, float currHealth)
    {
        Debug.Log(conn.identity.gameObject.name + "'s Health is " + currHealth);
        InGameUIManager.instance.HealthBar.UpdateHealthBar(currHealth);
    }

    private IEnumerator IsAttack(float time)
    {
        anim.SetBool("isAttack", true);
        yield return new WaitForSeconds(time);
        anim.SetBool("isAttack", false);
    }
    #endregion

    #region Bomb
    [Command]
    public void ThrowBomb()
    {
        //layar gelap di police
        bomCooldown = GameManager.Instance.bomCooldown;
    }
    #endregion 

    #region Hide
    public void ReplacePlayer(int index)
    {
        var oldPlayerConn = this.connectionToClient;
        var newPlayer = Instantiate(NetworkManagerLobby.singleton.spawnPrefabs[index], transform.position, transform.rotation);

        InGameCharacterPlayer player = newPlayer.GetComponent<InGameCharacterPlayer>();
        player.health = health;
        player.playerType = playerType;

        NetworkServer.ReplacePlayerForConnection(oldPlayerConn, newPlayer, true);

        //RpcSetCameraTransform(newPlayer.GetComponent<NetworkIdentity>().connectionToClient);

        StartCoroutine(DestroyPlayerDelay(.2f));

    }

    IEnumerator DestroyPlayerDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        NetworkServer.Destroy(this.gameObject);
    }

    [Command]
    private void CmdHide(int index)
    {
        if (objectFinder)
        {
            ObjectForHide target = objectFinder.GetTargetHide();

            if (target != null)
            {
                ReplacePlayer(index);
                CmdSetType(PlayerType.thief);
                Debug.Log("Success Hide");
            }
        }
    }

    public void Hide(int index)
    {
        CmdHide(index);
        CmdSetPlayerCharacter("");
    }

    [TargetRpc]
    private void RpcSetCameraTransform(NetworkConnection conn)
    {
        SetCameraTransform(conn.identity.gameObject);
    }

    public void ShowPopUpTextObjectForHide(bool isShow)
    {
        if (hasAuthority)
        {
            if (playerType == PlayerType.thief)
            {
                foreach (var obj in objectFinder.objects)
                {
                    obj.PopupText.gameObject.SetActive(isShow);
                }
            }
        }
    }

    #endregion

    #region Dead
    [ClientRpc]
    public void RpcDead(InGameCharacterPlayer player)
    {
        player.nicknameText.color = Color.red;
        //player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
        //player.playerFinder.GetComponent<CircleCollider2D>().enabled = false;
        //player.GetComponent<BoxCollider2D>().enabled = false;
        player.playerType = PlayerType.viewer;
        GameManager.Instance.RemoveFromThiefList(player);
    }

    [Command(requiresAuthority = false)]
    public void CmdDead(InGameCharacterPlayer player)
    {
        RpcDead(player);
    }
    #endregion

    #region Win Lose
    [TargetRpc]
    public void ShowPanelWin(NetworkConnection conn)
    {
        Debug.Log(conn.identity.name + " Win!");
        InGameUIManager.instance.ShowPanelWin();
    }

    [TargetRpc]
    public void ShowPanelLose(NetworkConnection conn)
    {
        Debug.Log(conn.identity.name + " Lose!");
        InGameUIManager.instance.ShowPanelLose();
    }
    #endregion
}
