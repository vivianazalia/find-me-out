using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class PlayerMovementTest
{
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene("RoomSnow");
    }

    [Test]
    public void PlayerFinderTest()
    {

    }

    //[UnityTest]
    //public IEnumerator MoveTest()
    //{
    //    var obj = Resources.Load("Prefabs/PlayerParticipant");
    //    var player = Object.Instantiate(obj) as GameObject;
    //    var currPos = player.transform.position;
    //    var movement = player.GetComponent<PlayerMovement>();
    //
    //    var input = player.GetComponent<InputManager>();
    //    Vector3 newPos = input.LeftStickInput + new Vector2(1, 1);
    //
    //    movement.Tick();
    //    yield return new WaitForSeconds(.2f);
    //    var nextPos = currPos + newPos;
    //
    //    Assert.AreNotEqual(currPos, nextPos);
    //    
    //}
}
