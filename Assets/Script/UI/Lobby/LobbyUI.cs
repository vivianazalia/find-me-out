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
        var manager = NetworkManager.singleton as NetworkManagerLobby;
        manager.gameRuleData = FindObjectOfType<GameData>().GetGameRuleData();

        var players = FindObjectsOfType<PlayerRoom>();
        for(int i = 0; i < players.Length; i++)
        {
            players[i].readyToBegin = true;
        }

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
