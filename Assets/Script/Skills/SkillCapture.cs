using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCapture : Skill
{
	void Start(){
		maxCooldown = 30;
	}
    protected override void OnInvoke()
    {
		
        //Buat ray yang ngecek apakah ada sesuatu di depan, ambil objek pertama. 
        //Kalau objeknya hider, ganti hidernya jadi ghost <-- ini gimanaaa (sementara pakai enum Role dulu aja. class Ghost/Hider/Seeker ngga kepake)
        throw new System.NotImplementedException();
		UpdateSkillState(SkillState.Cooldown);
    }
}
