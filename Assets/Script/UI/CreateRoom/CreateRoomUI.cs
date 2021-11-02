using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateRoomUI : MonoBehaviour
{
    [SerializeField] private List<Button> chooseMapButtons;

    [SerializeField] private List<Button> policeCountButtons;

    [SerializeField] private List<Button> maxPlayerCountButtons;

    private CreateGameRoomData roomData;

    // Start is called before the first frame update
    void Start()
    {
        roomData = new CreateGameRoomData { policeCount = 1, maxPlayerCount = 8 };   
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
    }

    public void UpdatePoliceCount(int count)
    {
        roomData.maxPlayerCount = count;

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
}

public class CreateGameRoomData
{
    public string map;
    public int policeCount;
    public int maxPlayerCount;
}
