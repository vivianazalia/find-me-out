using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;

    public Portal portal;

    public SpawnPosition policeSpawnPos;
    public SpawnPosition thiefSpawnPos;

    public GameState gameState;

    [SyncVar]
    public float bomCooldown;
    [SyncVar]
    public float shootCooldown;
    [SyncVar]
    public int bulletCount;
    [SyncVar]
    public float gameplayDuration;
    [SyncVar]
    public float hidingTime;
    [SyncVar]
    public float healthAmount;

    private bool isHidingDuration = true;

    public float minutes;
    public float seconds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public List<InGameCharacterPlayer> players = new List<InGameCharacterPlayer>();

    public List<InGameCharacterPlayer> thiefs = new List<InGameCharacterPlayer>();

    public void AddPlayer(InGameCharacterPlayer player)
    {
        if (!players.Contains(player))
        {
            players.Add(player);
        }
    }

    public void AddToThiefList(InGameCharacterPlayer thief)
    {
        if (!thiefs.Contains(thief))
        {
            thiefs.Add(thief);
            ThiefCount.instance.thiefCount += 1;
        }
    }

    public void RemoveFromThiefList(InGameCharacterPlayer thief)
    {
        if (thiefs.Contains(thief))
        {
            Debug.Log("Masuk remove list from player");
            thiefs.Remove(thief);
            ThiefCount.instance.thiefCount -= 1;
        }
        CheckGameOver();
    }

    public List<InGameCharacterPlayer> GetPlayerList()
    {
        return players;
    }

    public IEnumerator GameReady()
    {
        var manager = NetworkManager.singleton as NetworkManagerLobby;
        bomCooldown = manager.gameRuleData.bomCooldown;
        shootCooldown = manager.gameRuleData.shootCooldown;
        bulletCount = manager.gameRuleData.bulletCount;
        gameplayDuration = manager.gameRuleData.gameplayDuration;
        hidingTime = manager.gameRuleData.hidingTime;
        healthAmount = 1;

        while (manager.roomSlots.Count != players.Count)
        {
            yield return null;
        }

        SetRolePolice();

        yield return new WaitForSeconds(.1f);

        SetRoleThief();

        yield return new WaitForFixedUpdate();

        SetPlayerSpawnPosition();

        yield return new WaitForFixedUpdate();

        SpawnCharacterPlayer();

        yield return new WaitForSeconds(.5f);

        SetCooldownSkillPlayer();

        yield return new WaitForSeconds(1f);
    }

    private void SetRolePolice()
    {
        var manager = NetworkManager.singleton as NetworkManagerLobby;
        for (int i = 0; i < manager.policeCount; i++)
        {
            var player = players[Random.Range(0, players.Count)];
            if (player.playerType != PlayerType.police)
            {
                player.SetType(PlayerType.police);
            }
            else
            {
                i--;
            }
        }
    }

    private void SetRoleThief()
    {
        var manager = NetworkManager.singleton as NetworkManagerLobby;
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].playerType == PlayerType.participant)
            {
                //player.playerType = PlayerType.police;
                players[i].SetType(PlayerType.thief);
            }
        }
    }

    private void SetPlayerSpawnPosition()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].playerType == PlayerType.police)
            {
                Vector3 pos = policeSpawnPos.GetSpawnPosition();
                players[i].RpcPosition(pos);
            }
            else if (players[i].playerType == PlayerType.thief)
            {
                Vector3 pos = thiefSpawnPos.GetSpawnPosition();
                players[i].RpcPosition(pos);
            }
        }
    }

    private void SpawnCharacterPlayer()
    {
        foreach (var player in players)
        {
            if (player.playerType == PlayerType.thief)
            {
                player.ReplacePlayer(2);
                player.SetType(PlayerType.thief);
            }
            else if (player.playerType == PlayerType.police)
            {
                player.ReplacePlayer(1);
                player.SetType(PlayerType.police);
            }
        }
    }

    private void SetCooldownSkillPlayer()
    {
        foreach (var player in players)
        {
            player.SetSkillCooldown();
        }
    }

    private IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(2f);

        gameState = GameState.Start;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            StartCoroutine(GameReady());
        }

        gameState = GameState.Prepare;

        StartCoroutine(ChangeState());
    }

    private void Update()
    {
        if (isHidingDuration && gameState == GameState.Start)
        {
            if (hidingTime > 0)
            {
                hidingTime -= Time.deltaTime;
            }
            else
            {
                portal.OpenPortal();
                isHidingDuration = false;
            }
        }
        else if(gameState == GameState.Start)
        {
            if (gameplayDuration > 0)
            {
                gameplayDuration -= Time.deltaTime;
            }
            else
            {
                gameState = GameState.Over;
                //display panel win lose
            }
        }

        Timer();

        CheckGameOver();

        ExitGame();
    }

    public void CheckGameOver()
    {
        if (gameState == GameState.Start && thiefs.Count == 0 && ThiefCount.instance.thiefCount == 0)
        {
            //polisi menang
            foreach (var p in players)
            {
                if (p.playerType == PlayerType.police)
                {
                    p.state = WinLoseState.winner;
                }
                else
                {
                    p.state = WinLoseState.loser;
                }
            }

            gameState = GameState.Over;
        }
        else if (gameState == GameState.Over && ThiefCount.instance.thiefCount > 0)
        {
            //thief menang
            foreach (var p in players)
            {
                if (p.playerType == PlayerType.police)
                {
                    p.state = WinLoseState.loser;
                }
                else
                {
                    p.state = WinLoseState.winner;
                }
            }
        }
    }

    public void Timer()
    {
        if (isHidingDuration && gameState == GameState.Start)
        {
            //Debug.Log("is hiding duration : " + isHidingDuration);
            minutes = hidingTime / 60;
            seconds = hidingTime % 60;
        }
        else if(gameState == GameState.Start)
        {
            //Debug.Log("is hiding duration : " + isHidingDuration);
            minutes = gameplayDuration / 60;
            seconds = gameplayDuration % 60;
        }
        else
        {
            minutes = 0;
            seconds = 0;
        }
    }

    private void ExitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}

public enum GameState
{
    Prepare,
    Start,
    Over
}
