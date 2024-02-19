using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class VRGrab : NetworkBehaviour
{
    private NetworkObject networkObject;

    private void Awake()
    {
        networkObject = GetComponent<NetworkObject>();
    }

    private void OnGrab() // This method should be called when the VR controller grabs an object
    {
        Debug.Log("Grab detected");
        if (NetworkManager.Singleton.IsClient)
        {
            Debug.Log("Is Client");
            networkObject.ChangeOwnership(NetworkManager.Singleton.LocalClientId);
        }
    }
}