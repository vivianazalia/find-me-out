using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Mirror;
using Mirror.Discovery;

public class Testing 
{
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene("MainMenu");
    }

    [UnityTest]
    public IEnumerator CreateRoomTest()
    {
        var menu = GameObject.Find("MainMenuUI").GetComponent<MainMenu>();
        var currentScene = SceneManager.GetActiveScene().name;

        var manager = NetworkManager.singleton as NetworkManagerLobby;
        manager.GameplayScene = "RoomSnow";
        manager.maxConnections = 1;

        menu.CreateRoom();
        yield return new WaitForSeconds(1f);

        var nextScene = NetworkManager.singleton.onlineScene;

        Assert.AreNotEqual(nextScene, currentScene);
    }
}
