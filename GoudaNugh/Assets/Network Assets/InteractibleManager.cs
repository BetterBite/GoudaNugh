using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

public class InteractibleManager : NetworkBehaviour {
    public static InteractibleManager Instance { get; private set; }
    public NetworkPrefabsList ObjectsToSpawn;

    public void Awake() {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // NetworkConnect.cs subscribes this method to OnSceneEvent. https://docs-multiplayer.unity3d.com/netcode/current/basics/scenemanagement/scene-events/
    public void CheckSceneEvent(SceneEvent sceneEvent) {
        if (sceneEvent.SceneEventType == SceneEventType.LoadEventCompleted) OnSceneLoad();          // We check that the event received is LoadEventCompleted and then call OnSceneLoad
    }

    public void OnSceneLoad() {
        if (IsServer) { 
            var networkManager = NetworkManager.Singleton;
            Debug.Log("Loading Objects");
            if (networkManager == null) {
                Debug.LogError("NetworkManager is blerry missing!");
                return;
            }
            // Read https://docs-multiplayer.unity3d.com/netcode/current/basics/object-spawning/
            foreach (NetworkPrefab NetworkPrefab in ObjectsToSpawn.PrefabList) {
                var instance = Instantiate(NetworkPrefab.Prefab);
                var networkInstance = instance.GetComponent<NetworkObject>();
                networkInstance.Spawn();
                NetworkObjectReference objectReference = new NetworkObjectReference(networkInstance);
                InstantiatePastObjectRPC(objectReference);
                InstantiateFutureObjectRPC(objectReference);
            }
        }
    }

    /* 
    RPC Calls here. Make sure each has an attribute specifying to whom it is sent to
    See this Unity doc for all valid attributes https://docs.unity3d.com/Packages/com.unity.netcode.gameobjects@1.8/api/Unity.Netcode.SendTo.html
    
    Hint : To send an RPC call to be executed on the other player's machine, use SendTo.NotMe and check within the function if you are not the server
    See immediate example below (UpdateFutureObjectClientRPC)

    You DO NOT need to use RPC calls if you are dealing with NetworkVariables unless you don't own the object, in which case you need to use an RPC call to update the variable
     */

    // atm assume host is past player, client is future player
    [Rpc(SendTo.ClientsAndHost)]
    public void InstantiatePastObjectRPC(NetworkObjectReference objectReference) {
        // TODO - Add transforms to all Instantiate calls 
        if (IsServer) { //Check if you are the past player here
            objectReference.TryGet(out NetworkObject networkObject);
            GameObject Object = Instantiate(networkObject.gameObject.GetComponent<Variables>().PastObjectPrefab);
            Object.GetComponent<PastObject>().networkObject = networkObject;
            Object.GetComponent<PastObject>().Setup();
            TransferOwnerServerRPC(objectReference, NetworkManager.Singleton.LocalClientId);
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void InstantiateFutureObjectRPC(NetworkObjectReference objectReference) {
        // TODO - Add transforms to all Instantiate calls 
        if (IsServer) { //this line is uncommented when testing on one device
        //if (!IsServer) { //Check if you are the future player here
            objectReference.TryGet(out NetworkObject networkObject);
            GameObject Object = Instantiate(networkObject.gameObject.GetComponent<Variables>().FutureObjectPrefab);
            Object.GetComponent<FutureObject>().networkObject = networkObject;
            Object.GetComponent<FutureObject>().Setup();
            TransferOwnerServerRPC(objectReference, NetworkManager.Singleton.LocalClientId);
        }
    }

    [Rpc(SendTo.Server)]
    public void TransferOwnerServerRPC(NetworkObjectReference objectReference, ulong clientID) {
        objectReference.TryGet(out NetworkObject networkObject);
        networkObject.ChangeOwnership(clientID);
    }
}
