using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSearch : Skill
{
    protected override void Initialize()
    {
        this.skillName = "Search";
        this.useSelectPhase = false;
    }

    protected override void OnInvoke()
    {
        //Menampilkan semua footstep di sekitar
        //Semua footstep dalam area ini di masukkan ke list lalu di set visible. 
        Debug.Log($"Skill {skillName} Invoked!");
        UpdateSkillState(SkillState.Cooldown);
    }
}
