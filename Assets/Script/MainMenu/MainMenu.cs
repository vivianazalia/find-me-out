using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror.Discovery;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = null;
    [SerializeField] private NetworkDiscovery networkDiscovery;

    [Header("UI")]
    [SerializeField] private GameObject panelJoinGame;
    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private GameObject panelSettingGameplay;
    [SerializeField] private TMP_InputField networkAddrField;
    [SerializeField] private GameObject inputNickname;
    [SerializeField] private TMP_Text connectingText;
    public Button enterRoomButton;

    private void Start()
    {
        if(networkManager == null)
        {
            networkManager = FindObjectOfType<NetworkManagerLobby>();
            networkDiscovery = networkManager.GetComponent<NetworkDiscovery>();
        }

        if (!PlayerPrefs.HasKey(GameSetting.firstRunAppKey))
        {
            inputNickname.SetActive(true);
        }
        else
        {
            GameSetting.nickname = PlayerPrefs.GetString(GameSetting.playerPrefsNameKey);
            panelMainMenu.SetActive(true);
        }
    }

    private void Update()
    {
        if(networkAddrField.text != "")
        {
            enterRoomButton.gameObject.SetActive(true);
        }
        else
        {
            enterRoomButton.gameObject.SetActive(false);
        }

        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
            if (!NetworkClient.active)
            {
                connectingText.text = "";
            }
            else
            {
                connectingText.text = "Connecting...";
            }
            
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Quit();
            }
        }

    }

    public void StartGame()
    {
        panelJoinGame.SetActive(true);
    }

    public void CreateRoom()
    {
        if(networkManager.maxConnections != 0)
        {
            networkManager.minPlayers = 1;
            networkManager.networkAddress = "localhost";
            panelSettingGameplay.SetActive(false);
            networkManager.StartHost();
            networkDiscovery.AdvertiseServer();

            StartCoroutine(LoadSceneAsync());
        }
        Debug.Log("Host address : " + networkManager.networkAddress);
    }

    public void JoinRoom()
    {
        if (!NetworkClient.active)
        {
            networkManager.networkAddress = networkAddrField.text;
            networkManager.StartClient();
            
            //StartCoroutine(LoadSceneAsync());
        }
    }

    IEnumerator LoadSceneAsync()
    {
        loadingPanel.SetActive(true);
        while (!NetworkManager.loadingSceneAsync.isDone)
        {
            float progress = Mathf.Clamp01(NetworkManager.loadingSceneAsync.progress / .9f);
            loadingBar.value = progress;
            yield return null;
        }
    }

    private void OnApplicationQuit()
    {
        NetworkClient.Disconnect();
        NetworkManager.singleton.StopClient();
    }

    private void Quit()
    {
        Application.Quit();
    }
}
