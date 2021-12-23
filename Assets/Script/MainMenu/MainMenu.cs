using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject panelJoinGame;
    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private GameObject panelSettingGameplay;
    [SerializeField] private TMP_InputField networkAddrField;
    [SerializeField] private GameObject inputNickname;
    public Button enterRoomButton;

    private void Start()
    {
        if(networkManager == null)
        {
            networkManager = FindObjectOfType<NetworkManagerLobby>();
        }

        if (!PlayerPrefs.HasKey(PlayerSettings.firstRunAppKey))
        {
            inputNickname.SetActive(true);
        }
        else
        {
            PlayerSettings.nickname = PlayerPrefs.GetString(PlayerSettings.playerPrefsNameKey);
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
            panelSettingGameplay.SetActive(false);
            networkManager.StartHost();

            StartCoroutine(LoadSceneAsync());
        }
        Debug.Log("Host address : " + networkManager.networkAddress);
    }

    public void JoinRoom()
    {
        if (!NetworkClient.active)
        {
            if (networkAddrField.text != "")
            {
                networkManager.networkAddress = networkAddrField.text;
                networkManager.StartClient();

                StartCoroutine(LoadSceneAsync());
            }
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
}
