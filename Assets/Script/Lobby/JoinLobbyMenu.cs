using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
    //[SerializeField] private NetworkManagerLobby networkManager = null;
    //
    //[Header("UI")]
    //[SerializeField] private GameObject joinModePanel;
    //[SerializeField] private TMP_InputField inputCodeRoom;
    //[SerializeField] private Button joinButton;
    //
    //private void OnEnable()
    //{
    //    NetworkManagerLobby.OnClientConnected += HandleClientConnected;
    //    NetworkManagerLobby.OnClientDisconnected += HandleClientDisconnected;
    //}
    //
    //private void OnDisable()
    //{
    //    NetworkManagerLobby.OnClientConnected -= HandleClientConnected;
    //    NetworkManagerLobby.OnClientDisconnected -= HandleClientDisconnected;
    //}
    //
    //public void JoinLobby()
    //{
    //    if (!NetworkClient.active)
    //    {
    //        inputCodeRoom.text = "localhost";
    //
    //        networkManager.StartClient();
    //        networkManager.networkAddress = inputCodeRoom.text;
    //        joinModePanel.SetActive(false);
    //    }
    //}
    //
    //private void HandleClientConnected()
    //{
    //    Debug.Log("Handle Client Connected");
    //    joinButton.interactable = true;
    //}
    //
    //private void HandleClientDisconnected()
    //{
    //    Debug.Log("Handle Client DisConnected");
    //    joinButton.interactable = false;
    //}
}
