using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ServerSetup : MonoBehaviour
{
    public Configuration configuration;

    // Start is called before the first frame update
    void Start()
    {
        if (configuration.buildType == BuildType.REMOTE_SERVER)
        {
            StartServer();
        }
    }

    private void StartServer()
    {
        NetworkServer.Listen(7777);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
