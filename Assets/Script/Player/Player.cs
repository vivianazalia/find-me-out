using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager), typeof(PlayerMovement), typeof(AnimationController))]
public class Player : MonoBehaviour
{
	//public List<Skill> skills = new List<Skill>();

	PlayerSettings playerSettings;
	PlayerMovement playerMovement;
	GameObject currentChild, originalChild;
	bool isUsingDefaultChild;
	public PlayerRole Role { get { return playerSettings.Role; } }

	public bool IsUsingDefaultChild { get { return isUsingDefaultChild; } }

	public static event Action<PlayerRole, PlayerRole> OnPlayerRoleChanged;

	private void Awake()
	{
		gameObject.AddComponent<PlayerSettings>();
		playerSettings = GetComponent<PlayerSettings>();
		playerMovement = GetComponent<PlayerMovement>();
	}

	public void SetSettings(PlayerSettings s)
	{
		playerSettings = s;
	}

	public void AssignRole(PlayerRole role)
	{
		OnPlayerRoleChanged?.Invoke(playerSettings.Role, role);
		playerSettings.Role = role;
	}

	//public void PrepareRole()
	//{
	//	MenuManager.instance.ShowSkillUI(playerSettings.Role); //==================================== ini untuk player ini aja / local
	//	skills = MenuManager.instance.GetSkillsInButtons(playerSettings.Role);
	//	foreach (Skill s in skills)
	//	{
	//		s.SetOwner(this);
	//	}
	//	//Instantiate gameobject based on role. gameobject ambil dari game manager. originalChild = gameobject
	//	//lalu di parent ke this.transform
	//}

	public void SetChild(GameObject child)
	{
		//Get originalChild -> SetActive false
		originalChild?.SetActive(false);
		//Mungkin butuh instantiate(child)?
		//Set new child's parent as this.transform
		child.transform.parent = this.transform;
		currentChild = child;
		currentChild?.SetActive(true);
		isUsingDefaultChild = false;
	}

	public void UseDefaultChild()
	{
		currentChild?.SetActive(false);
		originalChild?.SetActive(true);
		//Mungkin butuh Destroy(currentChild)?
		isUsingDefaultChild = true;
	}

	public void SetDefaultChild(GameObject child)
	{
		originalChild = child;
	}

	public void SetMovementEnabled(bool enabled)
	{
		playerMovement.IsEnabled = enabled;
	}

	// Start is called before the first frame update
	void Start()
	{
		//GameManager.instance.AddPlayer(this);
	}
}
