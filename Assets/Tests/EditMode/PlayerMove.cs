using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class PlayerMove
{
    //[SetUp]
    //public void Setup()
    //{
    //    EditorSceneManager.OpenScene("MainMenu");
    //}

    [Test]
    public void PlayerTest()
    {
        var obj = new GameObject();
        var police = obj.AddComponent<InGameCharacterPlayer>();
        police.playerType = PlayerType.police;

        var enemyObj = new GameObject();
        var enemy = enemyObj.AddComponent<InGameCharacterPlayer>();
        enemy.playerType = PlayerType.thief;
        enemy.transform.position = police.transform.position + police.transform.forward;
        Debug.Log(police.transform.position);
        police.GetComponent<InGameCharacterPlayer>().Shoot();

        Assert.AreNotEqual(enemy.health, 1);
    }

}
