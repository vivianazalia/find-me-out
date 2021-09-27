using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTransform : Skill
{
    public override void Invoke()
    {
        //Buat ray, ambil objeknya, jika objeknya termasuk Transformable, ganti objek ini menjadi objek itu <-- how???
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        invoke_key = 't';
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
