using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject panelJoinGame;
    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private GameObject panelProfil;
    [SerializeField] private TMP_InputField networkAddrField;
    [SerializeField] private GameObject inputNickname;
    [SerializeField] private Button enterRoomButton;

    private void Start()
    {
        if(networkManager == null)
        {
            networkManager = FindObjectOfType<NetworkManager>();
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
        networkManager.StartHost();
        Debug.Log("Host address : " + networkManager.networkAddress);
    }

    public void JoinRoom()
    {
        if (!NetworkClient.active)
        {
            if (networkAddrField.text != "")
            {
                //cek jika kode yang dimasukkan tersedia atau tidak
                networkManager.networkAddress = networkAddrField.text;
                //panelJoinGame.SetActive(false);
                networkManager.StartClient();
            }
        }
    }

    public void Setting()
    {
        Debug.Log("Go to Setting Panel");
    }

    public void Profil()
    {
        panelProfil.SetActive(true);
    }
}
