using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovementTest
{
    [Test]
    public void PlayerMovementTesting()
    {
        GameObject player = (GameObject)Object.Instantiate(Resources.Load("./PlayerPolice"));
        PlayerMovement movement = player.GetComponent<PlayerMovement>();

        InputManager inputManager = movement.GetComponent<InputManager>();
        Vector3 pos = player.transform.position;

        movement.Tick();

        Vector3 currPos = player.transform.position;

        Assert.AreNotEqual(pos, currPos);
    }
}
