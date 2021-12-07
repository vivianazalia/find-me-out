using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCapture : Skill
{

    protected override void OnInvoke()
    {
        //Buat ray yang ngecek apakah ada sesuatu di depan, ambil objek pertama. 
        //Kalau objeknya hider, ganti hidernya jadi ghost <-- ini gimanaaa (sementara pakai enum Role dulu aja. class Ghost/Hider/Seeker ngga kepake)
        Debug.Log($"Skill {skillName} Invoked!");
        UpdateSkillState(SkillState.Cooldown);
        
    }

    protected override void Initialize()
    {
        this.skillName = "Capture";
        this.useSelectPhase = false;
    }
}
