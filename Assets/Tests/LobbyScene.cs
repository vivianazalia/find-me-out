using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Mirror;

public class LobbyScene
{
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene("LobbyRoom");
    }

    [UnityTest]
    public IEnumerator StartGameTest()
    {
        var menu = GameObject.Find("LobbyCanvas").GetComponent<LobbyUI>();
        Debug.Log("Menu: " + menu);
        var currentScene = SceneManager.GetActiveScene().name;

        menu.OnClickStartButton();

        var manager = NetworkManager.singleton as NetworkManagerLobby;
        var nextScene = manager.GameplayScene;

        yield return new WaitForSeconds(1f);

        Assert.AreNotEqual(nextScene, currentScene);
    }
}
