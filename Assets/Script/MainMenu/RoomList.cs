using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Discovery;
using UnityEngine.UI;

public class RoomList : MonoBehaviour
{
    public static RoomList instance = null;

    readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
    public NetworkDiscovery networkDiscovery;
    public NetworkManagerLobby networkManager;

    public GameObject roomlistPrefab;
    public Transform parent;

    //public ButtonRoom buttonSelected;

    [Header("UI")]
    [SerializeField] private GameObject panelSettingGameplay;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Slider loadingBar;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (networkDiscovery == null)
        {
            networkDiscovery = GetComponent<NetworkDiscovery>();
            UnityEditor.Events.UnityEventTools.AddPersistentListener(networkDiscovery.OnServerFound, OnDiscoveredServer);
            UnityEditor.Undo.RecordObjects(new Object[] { this, networkDiscovery }, "Set NetworkDiscovery");
        }
    }
#endif

    public void RefreshRoomList()
    {
        discoveredServers.Clear();
        networkDiscovery.StartDiscovery();
        ShowRoomList();
    }

    public void CreateRoom()
    {
        if (networkManager.maxConnections != 0)
        {
            networkManager.minPlayers = 4;
            panelSettingGameplay.SetActive(false);
            discoveredServers.Clear();
            //networkManager.StartServer();
            networkDiscovery.AdvertiseServer();

            StartCoroutine(LoadSceneAsync());
        }
        Debug.Log("Host address : " + networkManager.networkAddress);
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

    public void ShowRoomList()
    {        
        foreach (ServerResponse info in discoveredServers.Values)
        {
            var room = Instantiate(roomlistPrefab);
            room.transform.SetParent(parent);
            room.GetComponent<ButtonRoom>().info = info;
        }
    }

    //public void SelectionButton(ButtonRoom button)
    //{
    //    buttonSelected = button;
    //}

    public void JoinRoom()
    {
        //if (!buttonSelected)
        //{
        //    Debug.Log("Please choose room");
        //    return;
        //}
        //
        //Connect(buttonSelected);
        if (!NetworkClient.active)
        {
            networkManager.networkAddress = "20.102.124.216";
            networkManager.StartClient();
        }
    }

    public void Connect(ButtonRoom button)
    {
        if (!NetworkClient.active)
        {
            networkDiscovery.StopDiscovery();
            networkManager.StartClient(button.info.uri);
        }
    }

    public void OnDiscoveredServer(ServerResponse info)
    {
        Debug.Log("Server Found: " + info.serverId);
        if (discoveredServers.ContainsKey(info.serverId))
        {
            return;
        }

        discoveredServers[info.serverId] = info;

        
        ShowRoomList();
    }
}
