using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class Skill : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] protected Sprite readySprite, cancelSprite, cooldownSprite;
	[SerializeField] protected bool useSelectPhase;
	[SerializeField] protected string skillName;
	public string SkillName {get{return skillName;}}
	Image imageComponent;
	
	protected Player owner;
	
    [SerializeField] protected int maxCooldown;
    protected float cooldown; // Kalau 0 == ready
	bool quit = false;
	
	protected SkillState _skillState;
	public SkillState skillState {get{return _skillState;}}
	public bool IsReady {get{return (_skillState==SkillState.Ready);}}
	public static event Action<string, SkillState> OnSkillStateChanged;
	
	void Awake(){
		_skillState = SkillState.Cooldown;
		imageComponent = GetComponent<Image>();
	}
	
	public void SetOwner(Player p){
		owner = p;
	}
	
	public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
		if(IsReady){
			if(!useSelectPhase){
				UpdateSkillState(SkillState.Selected);
			}
			else{
				UpdateSkillState(SkillState.Invoked);
			}
		}
    }
	
	public void UpdateSkillState(SkillState newState){
		_skillState = newState;
		switch(newState){
			case SkillState.Ready:
				OnReady();
				break;
			case SkillState.Selected:
				OnSelected();
				break;
			case SkillState.Invoked:
				OnInvoke();
				break;
			case SkillState.Cooldown:
				StartCoroutine(OnCooldown());
				break;
			default:
				break;
		}
		OnSkillStateChanged?.Invoke(skillName, newState);
	}
	
	protected virtual void OnReady(){
		//Set icon clickable
		imageComponent.sprite = readySprite;
	}
	protected virtual void OnSelected(){
		//set icon cancel
		imageComponent.sprite = cancelSprite;
		//wait till something happens
		//next phase
	}
    protected virtual void OnInvoke(){
		//Effects
		//set icon cooldown
		
		UpdateSkillState(SkillState.Cooldown);
	}
	protected virtual IEnumerator OnCooldown(){
		imageComponent.sprite = cooldownSprite;
		cooldown = maxCooldown;
		while (cooldown > 0)
		{
			//Increment Timer until counter >= waitTime
			cooldown -= Time.deltaTime;
			//Check if we want to quit this function
			if (quit)
			{
				//Quit function
				yield break;
			}
			//Wait for a frame so that Unity doesn't freeze
			yield return null;
		}
		UpdateSkillState(SkillState.Ready);
	}
}

public enum SkillState {
		Ready,
		Selected,
		Invoked,
		Cooldown
	}