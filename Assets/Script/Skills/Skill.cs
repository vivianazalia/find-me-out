using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public abstract class Skill : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] protected Sprite readySprite, cancelSprite, cooldownSprite;
	[SerializeField] TMP_Text skillText;
	protected bool useSelectPhase;
	protected string skillName;
	public string SkillName {get{return skillName;}}
	Image imageComponent;
	
	protected Player owner;
	
    [SerializeField] protected int maxCooldown = 10;
    float cooldown; // Kalau 0 == ready
	bool quit = false;
	
	SkillState _skillState;
	public SkillState skillState {get{return _skillState;}}
	public bool IsReady {get{return (_skillState==SkillState.Ready);}}
	public static event Action<string, SkillState> OnSkillStateChanged;
	
	void Awake(){
		_skillState = SkillState.Ready;
		imageComponent = GetComponent<Image>();
		Initialize();
	}

	protected abstract void Initialize();

    public void SetOwner(Player p){
		owner = p;
	}
	
	public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name + $"Skill {skillName} is {skillState}");
		if(IsReady){
			if(useSelectPhase){
				UpdateSkillState(SkillState.Selected);
			}
			else{
				UpdateSkillState(SkillState.Invoked);
			}
		}
		else if(skillState == SkillState.Selected)
        {
			UpdateSkillState(SkillState.Ready);
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
		skillText.text = skillName + " Ready";
	}
	protected virtual void OnSelected(){
		//set icon cancel
		skillText.text = "Cancel";
		imageComponent.sprite = cancelSprite;
		//wait till something happens
		//next phase
	}
	protected abstract void OnInvoke();
	protected virtual IEnumerator OnCooldown(){
		imageComponent.sprite = cooldownSprite;
		cooldown = maxCooldown;
		Debug.Log($"Cooldown is set to {cooldown}");
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
			skillText.text = (Mathf.Floor(10*cooldown)/10).ToString();
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