using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager),typeof(PlayerMovement),typeof(AnimationController))]
public class Player : MonoBehaviour
{
    public List<Skill> skills = new List<Skill>();


    PlayerSettings playerSettings;
    PlayerMovement playerMovement;
    InputManager inputManager;
    AnimationController animationController;


    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
        animationController = GetComponent<AnimationController>();
        playerSettings = GetComponent<PlayerSettings>();
    }

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
        playerMovement.Tick();
        animationController.handleAnimation();
    }
}
