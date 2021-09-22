using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject joinModePanel;
    [SerializeField] private TMP_InputField inputCodeRoom;
    [SerializeField] private Button joinButton;

    private void OnEnable()
    {
        NetworkManagerLobby.OnClientConnected += HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected += HandleClientDisconnected;
    }

    private void OnDisable()
    {
        NetworkManagerLobby.OnClientConnected -= HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected -= HandleClientDisconnected;
    }

    public void JoinLobby()
    {
        string codeRoom = inputCodeRoom.text;

        networkManager.networkAddress = codeRoom;
        if (networkManager.isNetworkActive)
        {
            networkManager.StartClient();
            joinModePanel.SetActive(false);
        }
        else
        {
            Debug.Log("Server is off");
        }
    }

    private void HandleClientConnected()
    {
        Debug.Log("Handle Client Connected");
        joinButton.interactable = true;
    }

    private void HandleClientDisconnected()
    {
        Debug.Log("Handle Client DisConnected");
        joinButton.interactable = false;
    }
}
