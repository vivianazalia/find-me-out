using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance = null;

    private List<InGamePlayerMovement> players = new List<InGamePlayerMovement>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void AddPlayer(InGamePlayerMovement p)
    {
        if (!players.Contains(p))
        {
            players.Add(p);
        }
    }

    public void StartGame()
    {
        var roomplayers = FindObjectsOfType<PlayerRoom>();
        var players = FindObjectsOfType<LobbyPlayerMovement>();
        if(roomplayers.Length == players.Length)
        {
            Debug.Log("Roomplayer : " + roomplayers.Length + " players : " + players.Length);
            
        }
        //Debug.Log("Roomplayer : " + roomplayers.Length + " players : " + players.Length);
        SetRole();
    }

    public void SetRole()
    {
        var roomplayers = FindObjectsOfType<PlayerRoom>();
        var players = FindObjectsOfType<LobbyPlayerMovement>();
        for(int i = 0; i < roomplayers.Length; i++)
        {
            for(int j = 0; j < players.Length; j++)
            {
                Debug.Log(roomplayers[j].netId + " " + players[j].netId);
                if (roomplayers[j].netId == players[j].ownerNetId)
                {
                    Debug.Log("Masuk siniii sama");
                }
            }
        }
    }

    public IEnumerator GameReady()
    {
        var manager = NetworkManager.singleton as NetworkManagerLobby;
        while(manager.roomSlots.Count != players.Count)
        {
            Debug.Log("GameReady in GameManager");
            yield return null;
        }

        Debug.Log("Jalan ke siniii");
        Debug.Log("Roomplayer : " + manager.roomSlots.Count + " players : " + players.Count);

        for(int i = 0; i < manager.policeCount; i++)
        {
            Debug.Log("Looping ke-" + i);
            int rand = Random.Range(0, players.Count);
            if(players[rand].playerType != PlayerType.police)
            {
                players[rand].playerType = PlayerType.police;
            }
            else
            {
                i--;
            }
        }

        foreach (var player in players)
        {
            Debug.Log("player : " + player.playerType);
            if (player.playerType == PlayerType.participant)
            {
                player.playerType = PlayerType.thief;
                Debug.Log("Set player thief success : " + player.playerType);
            }
        }
    }

    private void Start()
    {
        if (isServer)
        {
            StartCoroutine(GameReady());
        }
    }
}
