using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected char invoke_key; 
    public int max_cooldown;
    protected int cooldown; // Kalau 0 == ready

    public abstract void Invoke(); //Habis invoke set cooldown ke max

    public char GetInvokeKey()
    {
        return invoke_key;
    }
    public bool IsReady()
    {
        return (cooldown == 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Kurangi cooldown tiap detik
    }
}
