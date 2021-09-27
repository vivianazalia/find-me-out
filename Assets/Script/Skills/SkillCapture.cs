using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCapture : Skill
{
    public override void Invoke()
    {
        //Buat ray yang ngecek apakah ada sesuatu di depan, ambil objek pertama. 
        //Kalau objeknya hider, ganti hidernya jadi ghost <-- ini gimanaaa (sementara pakai enum Role dulu aja. class Ghost/Hider/Seeker ngga kepake)
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        invoke_key = 'c';
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
