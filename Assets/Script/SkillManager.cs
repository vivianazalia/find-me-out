using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
	public static SkillManager instance;
	[SerializeField] List<Skill> hiderSkillSet, seekerSkillSet;
	public List<Skill> HiderSkillSet {get{return hiderSkillSet;}}
	public List<Skill> SeekerSkillSet {get{return seekerSkillSet;}}
	
	public List<Skill> GetSkillSet(PlayerRole role) {
		if(role==PlayerRole.Hider){
			return hiderSkillSet;
		}
		else if(role==PlayerRole.Seeker){
			return seekerSkillSet;
		}
		else{
			Debug.Log("SkillSet untuk role itu tidak ditemukan!");
			return null;
		}
	}
	void Awake(){
		instance = this;
	}
}