using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDistract : Skill
{
    protected override void Initialize()
    {
        this.skillName = "Distract";
        this.useSelectPhase = true;
    }

    protected override void OnInvoke()
    {
        //Instantiate koin, kasih velocity ke
        Debug.Log($"Skill {skillName} Invoked!");
        UpdateSkillState(SkillState.Cooldown);
    }
}
