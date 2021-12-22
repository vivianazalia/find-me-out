using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class CreateRoomUI : NetworkBehaviour
{
    [SerializeField] private List<Button> chooseMapButtons;

    [SerializeField] private List<Button> policeCountButtons;

    [SerializeField] private List<Button> maxPlayerCountButtons;

    public List<Button> GetChooseMapButton { get { return chooseMapButtons; } }
    public List<Button> GetPoliceCountButton { get { return policeCountButtons; } }

    private CreateGameRoomData roomData;

    public static CreateGameRoomData GetRoomData { get; set; }

    [Scene]
    public List<string> gameplaySceneList = new List<string>();

    void Start()
    {
        roomData = new CreateGameRoomData { policeCount = 1, maxPlayerCount = 8, map = EMap.Desert};   
    }

    public void UpdateMaxPlayerCount(int count)
    {
        roomData.maxPlayerCount = count;

        for(int i = 0; i < maxPlayerCountButtons.Count; i++)
        {
            if(i == count - 4)
            {
                maxPlayerCountButtons[i].image.color = new Color(.8f, .8f, .2f, 1f);
            }
            else
            {
                maxPlayerCountButtons[i].image.color = new Color(.8f, .8f, .2f, 0f);
            }
        }

        var manager = NetworkManager.singleton as NetworkManagerLobby;
        manager.maxConnections = roomData.maxPlayerCount;
    }

    public void UpdatePoliceCount(int count)
    {
        roomData.policeCount = count;

        var manager = NetworkManager.singleton as NetworkManagerLobby;
        manager.policeCount = count;

        for (int i = 0; i < policeCountButtons.Count; i++)
        {
            if (i == count - 1)
            {
                policeCountButtons[i].image.color = new Color(.8f, .8f, .2f, 1f);
            }
            else
            {
                policeCountButtons[i].image.color = new Color(.8f, .8f, .2f, 0f);
            }
        }

        int limitMaxPlayer = count == 1 ? 4 : count == 2 ? 5 : 7;
        if(roomData.maxPlayerCount < limitMaxPlayer)
        {
            UpdateMaxPlayerCount(limitMaxPlayer);
        }
        else
        {
            UpdateMaxPlayerCount(roomData.maxPlayerCount);
        }

        for(int i = 0; i < maxPlayerCountButtons.Count; i++)
        {
            var text = maxPlayerCountButtons[i].GetComponentInChildren<TMP_Text>();
            if (i < limitMaxPlayer - 4)
            {
                maxPlayerCountButtons[i].interactable = false;
                text.color = new Color(.4f, .5f, .5f, 1f);
            }
            else
            {
                maxPlayerCountButtons[i].interactable = true;
                text.color = new Color(.6f, .9f, .9f, 1f);
            }
        }
    }

    public void UpdateMap(int map)
    {
        roomData.map = (EMap)map;

        for (int i = 0; i < chooseMapButtons.Count; i++)
        {
            if (i == map)
            {
                chooseMapButtons[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                chooseMapButtons[i].image.color = new Color(1f, 1f, 1f, .5f);
            }
        }

        var manager = NetworkManager.singleton as NetworkManagerLobby;
        manager.GameplayScene = gameplaySceneList[map];
    }
}

public class CreateGameRoomData
{
    public EMap map;
    public int policeCount;
    public int maxPlayerCount;
}

public enum EMap
{
    Desert,
    Snow,
    Autumn
}
