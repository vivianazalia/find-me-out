using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ClientSetup : MonoBehaviour
{
    public Configuration configuration;
    public NetworkManager networkManager;
    public TelepathyTransport telepathyTransport;

    public void OnClientStart()
    {
        if(configuration.buildType == BuildType.REMOTE_CLIENT)
        {
            ConnectClient();
        }
    }

    private void ConnectClient()
    {
        networkManager.networkAddress = configuration.ipAddress;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
