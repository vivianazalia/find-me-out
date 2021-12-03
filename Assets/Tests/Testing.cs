using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class Testing
{
    [OneTimeSetUp]
    public void Setup()
    {
        SceneManager.LoadScene("MainMenu");
    }
   
    [UnityTest]
    public IEnumerator MainMenuEnable()
    {
        var obj = GameObject.Find("MainMenuUI").GetComponent<MainMenu>();
        bool enable = obj.enabled;
        Assert.IsTrue(enable);
        yield return null;
    }

    [UnityTest]
    public IEnumerator NetworkManager()
    {
        var manager = GameObject.Find("NetworkManager").GetComponent<NetworkManagerLobby>();
        Assert.NotNull(manager);

        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckNickname()
    {
        if(PlayerSettings.nickname != null)
        {
            Assert.NotNull(PlayerSettings.nickname);
        }
        yield return null;
    }
}
