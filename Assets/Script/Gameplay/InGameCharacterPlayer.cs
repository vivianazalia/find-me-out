using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

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

    [Header("UI")]
    [SerializeField] private Image healthSlider = null;

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
   
    [SyncVar(hook = nameof(SetIsLose_Hook))]
    public WinLoseState state;

    [SyncVar]
    public PlayerType playerType;

    public bool IsShootable { get { return shootCooldown < 0; } }
    public bool CanThrowBom { get { return bomCooldown < 0; } }

    public void SetIsLose_Hook(WinLoseState oldValue, WinLoseState newValue)
    {
        if (isLocalPlayer)
        {
            if (newValue == WinLoseState.winner)
            {
                ShowPanelWin();
            }
            else if (newValue == WinLoseState.loser)
            {
                ShowPanelLose();
            }
        }
    }

    public override void Start()
    {
        base.Start();
        if (hasAuthority)
        {
            CmdSetPlayerCharacter(GameSetting.nickname);
            SetUI();
        }

        GameManager.Instance.AddPlayer(this);

        state = WinLoseState.player;

        if (playerType == PlayerType.thief)
        {
            GameManager.Instance.AddToThiefList(this);
            IncreaseThiefCount();
        }
    }

    #region Thief Count
    private void IncreaseThiefCount()
    {
        if (isServer)
        {
            ThiefCount.instance.thiefCount += 1;
        }
    }

    private void DecreaseThiefCount()
    {
        if (isServer && ThiefCount.instance.thiefCount > 0 && playerType == PlayerType.thief)
        {
            ThiefCount.instance.thiefCount -= 1;
        }
    }
    #endregion

    private void SetUI()
    {
        if (playerType == PlayerType.police)
        {
            InGameUIManager.instance.ShootButton.Show(this);
            InGameUIManager.instance.ShootButton.Interactable(false);
        }
        else if (playerType == PlayerType.thief)
        {
            InGameUIManager.instance.ChangeToThiefButton.Show(this);
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

    public void SetPosition(Vector3 position)
    {
        if (isServer)
        {
            transform.position = position;
        }
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
                ShowUpdateHealthTarget(target);
                TakeDamage(target.connectionToClient, target.health);
                UpdateHealthBar(target.health, target);
            }
            else
            {
                //ghost mode
                Debug.Log(target.name + "'s health is " + target.health);
                target.Dead(); //masih error
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
        //Debug.Log(conn.identity.gameObject.name + "'s Health is " + currHealth);
        InGameUIManager.instance.HealthBar.UpdateHealthBar(currHealth);
    }

    private IEnumerator IsAttack(float time)
    {
        anim.SetBool("isAttack", true);
        yield return new WaitForSeconds(time);
        anim.SetBool("isAttack", false);
    }

    [TargetRpc]
    public void UpdateHealthBar(float currHealth, InGameCharacterPlayer target)
    {
        if (target.healthSlider)
        {
            Debug.Log("currhealth : " + currHealth);
            target.healthSlider.fillAmount = currHealth;
        }
    }

    [TargetRpc]
    public void ShowUpdateHealthTarget(InGameCharacterPlayer target)
    {
        StartCoroutine(ShowHealthTarget_Coroutine(3f, target));
    }

    private IEnumerator ShowHealthTarget_Coroutine(float duration, InGameCharacterPlayer target)
    {
        if (target.healthSlider)
        {
            Debug.Log("Masuk if coroutine");
            target.healthSlider.gameObject.SetActive(true);
            yield return new WaitForSeconds(duration);
            target.healthSlider.gameObject.SetActive(false);
        }
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
        var instanceRot = NetworkManagerLobby.singleton.spawnPrefabs[index].transform.rotation;
        var newPlayer = Instantiate(NetworkManagerLobby.singleton.spawnPrefabs[index], transform.position, instanceRot);

        InGameCharacterPlayer player = newPlayer.GetComponent<InGameCharacterPlayer>();
        player.health = health;
        player.playerType = playerType;

        NetworkServer.ReplacePlayerForConnection(oldPlayerConn, newPlayer, true);

        StartCoroutine(DestroyPlayerDelay(.2f));
    }

    IEnumerator DestroyPlayerDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (this.playerType == PlayerType.thief)
        {
            GameManager.Instance.RemoveFromThiefList(this);

            //if (isClient)
            //{
            //    CmdRemoveThiefCount();
            //}
        }

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

    [Command]
    public void ChangeToThief()
    {
        if (gameObject != NetworkManager.singleton.spawnPrefabs[2])
        {
            ReplacePlayer(2);
        }
    }

    #endregion

    #region Dead
    [ClientRpc]
    public void RpcDead()
    {
        if (nicknameText)
        {
            nicknameText.color = Color.red;
        }
        
        //player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
        //player.playerFinder.GetComponent<CircleCollider2D>().enabled = false;
        //player.GetComponent<BoxCollider2D>().enabled = false;
        //playerType = PlayerType.viewer;
        GameManager.Instance.RemoveFromThiefList(this);

        //ThiefCount.instance.UpdateThiefCount();
    }

    public void Dead()
    {
        DecreaseThiefCount();
        playerType = PlayerType.viewer;

        //ThiefCount.instance.UpdateThiefCount();
        //CmdUpdateThiefCount();
        RpcDead();
    }
    #endregion

    #region Win Lose
    //[TargetRpc]
    public void ShowPanelWin()
    {
        //Debug.Log(conn.identity.name + " Win!");
        InGameUIManager.instance.ShowPanelWin();
    }

    //[TargetRpc]
    public void ShowPanelLose()
    {
        //Debug.Log(conn.identity.name + " Lose!");
        InGameUIManager.instance.ShowPanelLose();
    }
    #endregion

    private void OnDestroy()
    {
        if(playerType == PlayerType.thief)
        {
            DecreaseThiefCount();
        }
        
    }
}
