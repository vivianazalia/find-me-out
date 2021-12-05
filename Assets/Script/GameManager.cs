using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    List<Player> players = new List<Player>();
	public List<Player> Players {get{return players;}}
    [SerializeField] List<GameObject> Maps = new List<GameObject>(); //Di set di unity
	public GameObject hiderModelPrefab, seekerModelPrefab, coinPrefab;
	public List<GameObject> disguiseableObjectList;
	
	//Script References
	[SerializeField] PlayerSettings playerSetting;
	[SerializeField] InputManager inputManager;
	[SerializeField] MenuManager menuManager;
	[SerializeField] SkillManager skillManager;
    
    GameState currentState;
    public GameState CurrentState { get { return currentState; } }
    public static event Action<GameState> OnGameStateChanged;
	
	struct Gamerule{
		public int playerCount;
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
    }

    void Start()
    {
        UpdateGameState(GameState.SelectMenu);
		rule = defaultRule;
		Player.OnPlayerRoleChanged += UpdateCount;
    }

    void Update()
    {
        StartCoroutine(TickSessionTime());
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
	
	void UpdateCount(PlayerRole prevRole, PlayerRole newRole){ //subscribe ke player OnRoleChanged
		AddToRole(prevRole, -1);
		AddToRole(newRole, 1);
	}
	
	void AddToRole(PlayerRole role, int value){
		switch(role){
			case PlayerRole.Unassigned:
				
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
			//Debug.Log("Seeker win");
		}
		else if(rule.sessionTime <= 0){
			//Debug.Log("Hider win");
		}
	}
	
	public void AddPlayer(Player p){
		if(players.Contains(p)) return;
		p.Initialize(playerSetting); 
		players.Add(p);
	}
	
	void AssignRoles(){
		//Pilih random seekerCount player
	}

    public void UpdateGameState(GameState newState)
    {
        currentState = newState;
        switch (newState) //Handle individual logic
        {
            case GameState.SelectMenu:
                HandleSelectMenu();
                break;
            case GameState.SelectRoom:
                HandleSelectRoom();
                break;
            case GameState.SelectMap:
                HandleSelectMap();
                break;
            case GameState.InLobby:
                HandleInLobby();
                break;
            case GameState.Play:
                HandlePlay();
                break;
            case GameState.Win:
                HandleWin();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            default:
                break;
        }
        OnGameStateChanged?.Invoke(newState); //Notify semua fungsi lain yg perlu tahu
    }

    private void HandleLose()
    {
        //Panggil MenuManager lalu tampilkan Canvas LOSE (Di canvas LOSE ada button untuk kembali ke lobby)
    }

    private void HandleInLobby()
    {
        //MenuManager tampilkan Canvas LobbyMenu
    }

    private void HandleSelectMap()
    {
        //MenuManager tampilkan Canvas SelectMap
    }

    private void HandleSelectRoom()
    {
        //MenuManager tampilkan Canvas SelectRoom yang isinya input field untuk memasukkan RoomID atau buat room
    }

    private void HandleSelectMenu() //Di call pertama!!!
    {
        
    }

    private void HandleWin()
    {
        //Panggil MenuManager lalu tampilkan Canvas LOSE (Di canvas LOSE ada button untuk kembali ke lobby)
    }

    private void HandlePlay()
    {
		//Pertama kali saja: AssignRoles();
		//Semua Seeker di disable InputManagernya selama 10 detik
    }
}

public enum GameState
{
    SelectMenu,
    SelectRoom,
    SelectMap,
    InLobby,
    Play,
    Win,
    Lose
}
