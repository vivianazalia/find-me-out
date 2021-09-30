using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IPlayerInput
{
    public char lastKeyPressed { get; private set; }

    public float horizontalAxis { get; private set; }

    public float verticalAxis { get; private set; }

    public void ReadInput()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");
        lastKeyPressed = Input.inputString[0];
    }
}
