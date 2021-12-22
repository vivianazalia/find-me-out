using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;

public class LobbyUI : NetworkBehaviour
{
    public static LobbyUI instance;

    [SerializeField] private Button startGameButton;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Slider loadingBar;

    [SerializeField]
    private GameLobbyPlayerCounter gameLobbyPlayerCounter;
    public GameLobbyPlayerCounter GameLobbyPlayerCounter { get { return gameLobbyPlayerCounter; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void ActiveStartButton()
    {
        startGameButton.gameObject.SetActive(true);
    }

    public void SetInteractableStartButton(bool isInteractable)
    {
        startGameButton.interactable = isInteractable;
    }

    public void OnClickStartButton()
    {
        var players = FindObjectsOfType<PlayerRoom>();
        for(int i = 0; i < players.Length; i++)
        {
            players[i].readyToBegin = true;
        }

        var manager = NetworkManager.singleton as NetworkManagerLobby;
        //for (int i = 0; i < manager.policeCount; i++)
        //{
        //    Debug.Log("Looping ke-" + i);
        //    int rand = Random.Range(0, players.Length);
        //    players[rand].SetPlayerType(PlayerType.police);
        //    Debug.Log(rand + " Masukk sini");
        //}
        //
        //foreach(var player in players)
        //{
        //    Debug.Log("player : " + player.playerType);
        //    if(player.playerType == PlayerType.participant)
        //    {
        //        player.SetPlayerType(PlayerType.thief);
        //        Debug.Log("Set player thief success : " + player.playerType);
        //    }
        //}
        manager.ServerChangeScene(manager.GameplayScene);
        
        StartCoroutine(LoadSceneAsync());
        
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
