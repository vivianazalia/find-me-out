using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInput
{
    void ReadInput();
    char lastKeyPressed { get; }
    float horizontalAxis { get; }
    float verticalAxis { get; }
}
