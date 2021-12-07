using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    List<Player> players = new List<Player>();
    List<Player> seekerList = new List<Player>();
    List<Player> hiderList = new List<Player>();
	public List<Player> Players {get{return players;}}
    [SerializeField] List<GameObject> Maps = new List<GameObject>(); //Di set di unity
	public GameObject hiderModelPrefab, seekerModelPrefab, coinPrefab;
	public List<GameObject> disguiseableObjectList;
	
	//Script References
	[SerializeField] PlayerSettings playerSetting;
    
    GameState currentState;
    public GameState CurrentState { get { return currentState; } }
    public static event Action<GameState> OnGameStateChanged;
	[SerializeField] GameState firstGameStateDebugOnly = GameState.SelectMenu;
	
	struct Gamerule{
		public int mapID;
		public int playerCount;
		public int participantCount;
		public int seekerCount;
		public int hiderCount;
		public int ghostCount;
		public float seekerFreezeTime;
		public float sessionTime;
	};
	[SerializeField] Gamerule defaultRule;
	Gamerule rule;

    private void Awake()
    {
        instance = this;
		Player.OnPlayerRoleChanged += UpdateCount;
    }
	
	void OnDestroy(){
		Player.OnPlayerRoleChanged -= UpdateCount;
	}

    void Start()
    {
        UpdateGameState(firstGameStateDebugOnly);
		rule = defaultRule;
    }
	
	IEnumerator TickSessionTime(){
		rule.sessionTime = defaultRule.sessionTime;
		while (rule.sessionTime > 0)
		{
			//Increment Timer until counter >= waitTime
			rule.sessionTime -= Time.deltaTime;
			//Wait for a frame so that Unity doesn't freeze
			yield return null;
		}
	}
	
	IEnumerator TickFreezeTime(){
		rule.seekerFreezeTime = defaultRule.seekerFreezeTime;
		while (rule.seekerFreezeTime > 0)
		{
			//Increment Timer until counter >= waitTime
			rule.seekerFreezeTime -= Time.deltaTime;
			//Wait for a frame so that Unity doesn't freeze
			yield return null;
		}
	}
	
	void UpdateCount(PlayerRole prevRole, PlayerRole newRole){ //subscribe ke player OnRoleChanged
		AddToRole(prevRole, -1);
		AddToRole(newRole, 1);
		if(currentState==GameState.Play){
			CheckWin();
		}
	}
	
	void AddToRole(PlayerRole role, int value){
		switch(role){
			case PlayerRole.Unassigned:
				rule.participantCount += value;
				break;
			case PlayerRole.Hider:
				rule.hiderCount += value;
				break;
			case PlayerRole.Seeker:
				rule.seekerCount += value;
				break;
			case PlayerRole.Ghost:
				rule.ghostCount += value;
				break;
			default:
				break;
		}
	}
	
	void CheckWin(){
		if(rule.hiderCount <= 0){
			UpdateGameState(GameState.SeekerWin);
		}
		else if(rule.sessionTime <= 0){
			UpdateGameState(GameState.SeekerLose);
		}
	}
	
	public void AddPlayer(Player p){
		if(players.Contains(p)) return;
		p.SetSettings(playerSetting); 
		players.Add(p);
		Debug.Log(players);
	}
	
	void GetSelectedValues(){
		rule.playerCount = MenuManager.instance.SelectedPlayerCount;
		rule.seekerCount = MenuManager.instance.SelectedSeekerCount;
		rule.hiderCount = rule.playerCount - rule.seekerCount;
		rule.mapID = MenuManager.instance.SelectedMapID;
	}
	
	void GoToMapID(int mapID){
		//Go to scene yg sesuai map ID yg dipilih
	}
	
	void AssignRoles(){
		int seekerLeft = rule.seekerCount;
		while(seekerLeft > 0){
			int rand = UnityEngine.Random.Range(0, players.Count - 1);
			Debug.Log(rand);
			if(players[rand].Role != PlayerRole.Seeker){
				players[rand].AssignRole(PlayerRole.Seeker);
				seekerList.Add(players[rand]);
				seekerLeft--;
			}
		}
		foreach(Player p in players){
			if(p.Role != PlayerRole.Seeker){
				p.AssignRole(PlayerRole.Hider);
				hiderList.Add(p);
			}
			p.PrepareRole();
		}
	}
	
	void SetAllMovementEnabled(List<Player> playerList, bool enabled){
		foreach(Player p in playerList){
			p.SetMovementEnabled(enabled);
		}
	}

    public void UpdateGameState(GameState newState)
    {
        currentState = newState;
        switch (newState) //Handle individual logic
        {
            case GameState.SelectMenu:
                HandleSelectMenu();
                break;
            case GameState.Settings:
                HandleSettings();
                break;
            case GameState.SelectRoom:
                HandleSelectRoom();
                break;
            case GameState.CreateRoom:
                HandleCreateRoom();
                break;
            case GameState.JoinRoom:
                HandleJoinRoom();
                break;
            case GameState.InLobby:
                HandleInLobby();
                break;
            case GameState.Play:
                StartCoroutine(HandlePlay());
                break;
            case GameState.SeekerWin:
                HandleSeekerWin();
                break;
            case GameState.SeekerLose:
                HandleSeekerLose();
                break;
            default:
                break;
        }
        OnGameStateChanged?.Invoke(newState); //Notify semua fungsi lain yg perlu tahu
    }
	
	private void HandleSelectMenu() //Di call pertama!!!
    {
        
    }

    private void HandleSettings()
    {
        
    }

    private void HandleSelectRoom()
    {
        
    }

    private void HandleCreateRoom()
    {
        
    }

    private void HandleJoinRoom()
    {
        
    }

    private void HandleInLobby()
    {
        
    }

    private IEnumerator HandlePlay() //Kalau butuh step by step dijadikan async aja. Lalu ditambah task.await. Atau dijadikan return IEnumerator
    {
		GetSelectedValues();
		AssignRoles();
		GoToMapID(rule.mapID);
		SetAllMovementEnabled(hiderList, true);
		SetAllMovementEnabled(seekerList, false);
		//Semua Seeker di disable InputManagernya selama 10 detik
		//Nyalakan timer
		yield return StartCoroutine(TickFreezeTime());
		//Enable Seeker
		if(rule.seekerFreezeTime <= 0){
			SetAllMovementEnabled(seekerList, true);
		}
		yield return StartCoroutine(TickSessionTime());
    }

    private void HandleSeekerLose()
    {
        
    }

    private void HandleSeekerWin()
    {
        
    }
}

public enum GameState
{
    SelectMenu,
	Settings,
	SelectRoom,
    CreateRoom, //Select map di sini
    JoinRoom,
    InLobby,
    Play,
    SeekerWin,
    SeekerLose
}
