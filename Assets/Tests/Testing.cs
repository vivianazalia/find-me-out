using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Mirror;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

        var nextScene = manager.onlineScene; //NetworkManager.singleton.onlineScene;

        Assert.AreNotEqual(nextScene, currentScene);
    }

    //[Test]
    //public void ClickSettingButton()
    //{
    //    var settingBtn = GameObject.Find("LobbyCanvas").transform.Find("Setting");
    //
    //    Assert.IsNotNull(settingBtn);
    //
    //    EventSystem.current.SetSelectedGameObject(settingBtn.gameObject);
    //
    //    settingBtn.GetComponent<Button>().onClick.Invoke();
    //    //var chara = GameObject.FindObjectOfType<LobbyPlayerMovement>();
    //    //Assert.IsNotNull(chara);
    //    //Debug.Log("Name : " + chara.name);
    //    
    //}
}
