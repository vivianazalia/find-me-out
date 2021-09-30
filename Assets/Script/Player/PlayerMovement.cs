using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    readonly IPlayerInput playerInput;
    readonly Transform transform;
    readonly PlayerSettings playerSettings;

    public PlayerMovement(IPlayerInput playerInput, Transform transform, PlayerSettings playerSettings)
    {
        this.playerInput = playerInput;
        this.transform = transform;
        this.playerSettings = playerSettings;
    }

    public void Tick()
    {
        //Geraknya player di sini, gerakkan transform menggunakan playerInput sebanyak moveSpeed di playerSettings
        
    }
}
