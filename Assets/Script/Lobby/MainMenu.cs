using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager = null;

    private string firstRunAppKey = "FirstRunApp";

    [Header("UI")]
    [SerializeField] private GameObject panelInputName;
    [SerializeField] private GameObject panelJoinGame;
    [SerializeField] private TMP_InputField networkAddrField;

    private void Start()
    {
        if(networkManager == null)
        {
            networkManager = FindObjectOfType<NetworkManager>();
        }
    }

    public void StartGame()
    {
        if (PlayerPrefs.HasKey(firstRunAppKey))
        {
            panelJoinGame.SetActive(true);
        }
        else
        {
            panelInputName.SetActive(true);
            PlayerPrefs.SetInt(firstRunAppKey, 1);
        }
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
            networkManager.networkAddress = networkAddrField.text;
            networkManager.StartClient();
        }
    }

    public void Setting()
    {
        Debug.Log("Go to Setting Panel");
    }

}
