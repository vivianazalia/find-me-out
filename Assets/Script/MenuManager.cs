using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour //Per player 1?
{
    //Reference ke Menu panel. 
    [SerializeField] GameObject selectMenu, selectRoomMenu, joinRoomMenu, createRoomMenu, lobbyMenu, settingsMenu, controlGUI, winLoseGUI, hiderSkillGUI, seekerSkillGUI;
	
	List<SkillButton> _skillButtons = new List<SkillButton>();
	public List<SkillButton> skillButtons {get{return _skillButtons;}}
	
	public static MenuManager instance;
	
	void Awake()
	{
		instance = this;
		GameManager.OnGameStateChanged += HandleGameStateChanged;
		Skill.OnSkillStateChanged += HandleSkillStateChanged;
		//_skillButtons = FindObjectsOfType<SkillButton>(); //Find objects yg childnya skillGUI. Atau apa harusnya pakai tag?
	}
	
	void OnDestroy(){
		GameManager.OnGameStateChanged -= HandleGameStateChanged;
		Skill.OnSkillStateChanged -= HandleSkillStateChanged; //Masih belum kepake. Skill state change bisa dimnitor lewat sini
	}
	
	public void ShowSkillUI(PlayerRole role){
		hiderSkillGUI?.SetActive(role==PlayerRole.Hider);
		seekerSkillGUI?.SetActive(role==PlayerRole.Seeker);
	}
	
	void HandleGameStateChanged(GameState newState){
		selectMenu?.SetActive(newState == GameState.SelectMenu);
		selectRoomMenu?.SetActive(newState == GameState.SelectRoom);
		joinRoomMenu?.SetActive(newState == GameState.JoinRoom);
		createRoomMenu?.SetActive(newState == GameState.CreateRoom);
		lobbyMenu?.SetActive(newState == GameState.InLobby);
		settingsMenu?.SetActive(newState == GameState.Settings);
		controlGUI?.SetActive(newState == GameState.Play);
		if(newState != GameState.Play){
			hiderSkillGUI?.SetActive(false);
			seekerSkillGUI?.SetActive(false);
		}
		winLoseGUI.SetActive(newState == GameState.SeekerWin || newState == GameState.SeekerLose);
		if(newState == GameState.SeekerWin){
			ShowSeekerWin();
		}
		else if(newState == GameState.SeekerLose){
			ShowSeekerLose();
		}
	}
	
	void ShowSeekerWin(){
		//Ke winLoseGUI -> cari child Text object -> ganti text ke Seeker Win
	}
	
	void ShowSeekerLose(){
		
	}
	
	void HandleSkillStateChanged(string skillName, SkillState state){
		Debug.Log($"{skillName}'s state is {state}");
	}

	public List<Skill> GetSkillsInButtons(PlayerRole role){
		List<Skill> skills = new List<Skill>();
		if(role==PlayerRole.Hider){
			foreach (Transform child in hiderSkillGUI.transform.GetComponentsInChildren<Transform>()) {
				skills.Add(child.gameObject.GetComponent<Skill>());
			}
		}
		else if(role==PlayerRole.Seeker){
			foreach (Transform child in seekerSkillGUI.transform.GetComponentsInChildren<Transform>()) {
				skills.Add(child.gameObject.GetComponent<Skill>());
			}
		}
		return skills;
	}
	
	//============================= BUTTONS =============================
	
	//===================== generic
	
	public void GoToGameState(GameState state){ //Alternative untuk semua button yang ganti game state
		GameManager.instance.UpdateGameState(state);
	}
	
	public void OnClickBackButton(GameState currentState){ //Tempatnya di mana2 kecuali selectMenu
		if(currentState == GameState.Settings) GameManager.instance.UpdateGameState(GameState.SelectMenu);
		if(currentState == GameState.SelectRoom) GameManager.instance.UpdateGameState(GameState.SelectMenu);
		if(currentState == GameState.CreateRoom) GameManager.instance.UpdateGameState(GameState.SelectRoom);
		if(currentState == GameState.JoinRoom) GameManager.instance.UpdateGameState(GameState.SelectRoom);
		if(currentState == GameState.InLobby) GameManager.instance.UpdateGameState(GameState.SelectRoom);
	}
	
	public void OnClickSettings(){ //Tempatnya di lobbyMenu, selectMenu, dan play
		//Question: Apa yg terjadi jika UpdateGameState ke Settings ketika lagi play?
		//GameManager.instance.UpdateGameState(GameState.Settings);
	}
	
	//===================== selectRoomMenu
	
	public void OnClickJoinRoom(){ //Tempatnya di selectRoom
		//Cek input field apakah sudah terisi
		//Cari host dengan alamat itu
		//Join
		GameManager.instance.UpdateGameState(GameState.InLobby); //Question: Client juga butuh update game state kah?
	}
	
	public void OnClickCreateRoom(){ //Tempatnya di selectRoom
		GameManager.instance.UpdateGameState(GameState.CreateRoom);
	}
	
	//=========================== createRoomMenu
	
	int selectedPlayerCount = 8, selectedSeekerCount = 1, selectedMapID = 0;
	public int SelectedPlayerCount {get{return selectedPlayerCount;}}
	public int SelectedSeekerCount {get{return selectedSeekerCount;}}
	public int SelectedMapID {get{return selectedMapID;}}
	
	public void OnClickSelectPlayerCount(int newPlayerCount){
		selectedPlayerCount = newPlayerCount;
		//Feedback ke button
	}
	
	public void OnClickSelectSeekerCount(int newSeekerCount){
		if((newSeekerCount * 3) > selectedPlayerCount){
			OnClickSelectPlayerCount(newSeekerCount * 3);
		} 
		selectedSeekerCount = newSeekerCount;
		//Feedback ke button
	}
	
	public void OnClickSelectMap(int newSelectedMapID){ 
		selectedMapID = newSelectedMapID;
		//Feedback ke button
	}
	
	public void OnClickConfirmCreateRoom(){
		//Create room
		//join room
		GameManager.instance.UpdateGameState(GameState.InLobby);
	}
	
	//=========================== lobbyMenu
	
	public void OnClickPlay(){ //Tempatnya di lobbyMenu, ditampilkan hanya untuk Host
		//Jika sudah ready semua, go ke state selanjutnya
		GameManager.instance.UpdateGameState(GameState.Play);
	}
	
	//=============================== winLoseGUI
	
	public void OnClickBackToLobby(){ //tempatnya di winLoseGUI
		//To DO: Pindah scene ke lobby
		GameManager.instance.UpdateGameState(GameState.InLobby);
	}
}
