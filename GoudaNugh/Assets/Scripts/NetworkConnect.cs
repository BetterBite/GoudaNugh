using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkConnect : MonoBehaviour
{

    public Transform p1Pos;
    public Transform p2Pos;
    public Transform rig;
    public void Create()
    {
       NetworkManager.Singleton.StartHost(); 
       rig.position = p1Pos.position;
    }

    public void Join()
    {
        NetworkManager.Singleton.StartClient();
        rig.position = p2Pos.position;

    }
}
