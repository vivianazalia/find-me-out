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
    //
    //[UnityTest]
    //public IEnumerator MainMenuEnable()
    //{
    //    var mainMenu = GameObject.Find("MainMenuUI").GetComponent<MainMenu>();
    //    bool enable = mainMenu.enabled;
    //
    //    Assert.IsTrue(enable);
    //
    //    yield return null;
    //}
    //
    //[UnityTest]
    //public IEnumerator NetworkManager()
    //{
    //    var manager = GameObject.Find("NetworkManager").GetComponent<NetworkManagerLobby>();
    //    Assert.NotNull(manager);
    //
    //    yield return null;
    //}
    //
    //[UnityTest]
    //public IEnumerator AssignPoliceCountInNetworkManager()
    //{
    //    //var mainMenu = GameObject.Find("MainMenuUI").transform.GetChild(0).gameObject;
    //    //var settingGameplay = mainMenu.transform.Find("SettingGameplay").gameObject;
    //    //var roomUI = settingGameplay.GetComponent<CreateRoomUI>();
    //    
    //    //roomUI.UpdatePoliceCount(1);
    //    CreateRoomUI.GetRoomData = new CreateGameRoomData
    //    {
    //        policeCount = 1
    //    };
    //    int policeCount = CreateRoomUI.GetRoomData.policeCount;
    //
    //    var manager = GameObject.Find("NetworkManager").GetComponent<NetworkManagerLobby>();
    //
    //    manager.policeCount = 1;
    //
    //    Assert.IsTrue(policeCount == manager.policeCount);
    //
    //    yield return null;
    //}
    //
    //[UnityTest]
    //public IEnumerator CheckNickname()
    //{
    //    if(PlayerSettings.nickname != null)
    //    {
    //        Assert.NotNull(PlayerSettings.nickname);
    //    }
    //    yield return null;
    //}

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

    //[UnityTest]
    //public IEnumerator JoinRoomTest()
    //{
    //    var join = GameObject.Find("JoinGame").GetComponent<RoomList>();
    //
    //    ButtonRoom button = new ButtonRoom();
    //    ServerResponse info = new ServerResponse { uri = button.info.uri };
    //    button.info = info;
    //
    //    yield return null;
    //
    //
    //}
}
