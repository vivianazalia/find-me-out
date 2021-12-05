using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager),typeof(PlayerMovement),typeof(AnimationController))]
public class Player : MonoBehaviour
{
    public List<Skill> skills = new List<Skill>();

    PlayerSettings playerSettings;
	GameObject currentPrefab;
	
	public static event Action<PlayerRole, PlayerRole> OnPlayerRoleChanged;

    private void Awake()
    {
		gameObject.AddComponent<PlayerSettings>();
        playerSettings = GetComponent<PlayerSettings>();
    }
	
	public void Initialize(PlayerSettings s){
		playerSettings = s;///////////////////////////////////////////Ini bener ta???
	}
	
	public void AssignRole(PlayerRole role){
		OnPlayerRoleChanged?.Invoke(playerSettings.Role, role);
		playerSettings.Role = role;
		MenuManager.instance.ShowSkillUI(role);
		skills = MenuManager.instance.GetSkillsInButtons(role);
		foreach(Skill s in skills){
			s.SetOwner(this);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.AddPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
