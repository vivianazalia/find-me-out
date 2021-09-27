using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int lives = 2;
    public List<Skill> skills = new List<Skill>(); 
    public enum Role
    {
        Unassigned,
        Hider,
        Seeker,
        Ghost
    }
    public Role role = Role.Unassigned;

    public void UseSkill(char c)
    {
        //Cari 'c' itu buat mengaktifkan skill mana
        foreach(var s in skills)
        {
            if (s.GetInvokeKey() == c)
            {
                s.Invoke();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
