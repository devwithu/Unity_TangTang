using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ConnectToServer : MonoBehaviour
{
    public bool autoConnect;
    public string ServerAddr;
    
    private void Start()
    {

        if (autoConnect)
        {
            GameObject.Find("NetworkManager").GetComponent<NetworkManager>().networkAddress = ServerAddr;
            GameObject.Find("NetworkManager").GetComponent<NetworkManager>().StartClient();
            Debug.Log("StartClient");
        }
    }
}
