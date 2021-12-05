using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour //Per player 1?
{
    //Reference ke Menu panel. 
    [SerializeField] GameObject menuPanel, controlGUI, hiderSkillGUI, seekerSkillGUI;
	[SerializeField] Text text;
	
	List<SkillButton> _skillButtons = new List<SkillButton>();
	public List<SkillButton> skillButtons {get{return _skillButtons;}}
	
	public static MenuManager instance;
	
	void Awake()
	{
		instance = this;
		//_skillButtons = FindObjectsOfType<SkillButton>(); //Find objects yg childnya skillGUI. Atau apa harusnya pakai tag?
	}
	
	public void ShowSkillUI(PlayerRole role){
		if(role==PlayerRole.Hider){
			ShowHiderGUI();
		}
		else if(role==PlayerRole.Seeker){
			ShowSeekerGUI();
		}
	}
	
	public void ShowHiderGUI(){
		hiderSkillGUI.GetComponent<Canvas>().enabled = true;
		seekerSkillGUI.GetComponent<Canvas>().enabled = false;
		//Atau pakai gameObject.SetActive?
	}
	public void ShowSeekerGUI(){
		hiderSkillGUI.GetComponent<Canvas>().enabled = false;
		seekerSkillGUI.GetComponent<Canvas>().enabled = true;
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
}
