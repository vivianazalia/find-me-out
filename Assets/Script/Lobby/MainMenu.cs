using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = null;

    private string firstRunAppKey = "FirstRunApp";

    [Header("UI")]
    [SerializeField] private GameObject panelInputName;
    [SerializeField] private GameObject panelJoinGame;

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

    public void HostLobby()
    {
        networkManager.StartHost();
        Debug.Log("Host address : " + networkManager.networkAddress);
    }

}
