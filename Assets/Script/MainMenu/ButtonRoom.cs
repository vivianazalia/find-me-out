using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror.Discovery;
using Mirror;
using TMPro;

public class ButtonRoom : MonoBehaviour
{
    private Button button;

    [SerializeField] private TMP_Text hostName;
    [SerializeField] private TMP_Text playerCount;

    public ServerResponse info;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        hostName.text = "Room";
        playerCount.text = NetworkManagerLobby.singleton.maxConnections.ToString();
    }

    public void OnClickButton()
    {
        var roomList = FindObjectOfType<RoomList>();
        Debug.Log("roomlist : " + roomList);
        roomList.Connect(this);
        //RoomList.instance.SelectionButton(this);
    }
}
