using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;
using Random = UnityEngine.Random;

public class Spawner : NetworkBehaviour {
    public static Spawner Singleton { get; private set; }
    public NetworkPrefabsList ObjectsToSpawn;
    public GameObject[] LocalFutureObjects;
    public int[] SafeCode { get; private set; }

    // Used for testing in TestingSuite, toggle it to force network spawning (Does not use RelayServices)
    [SerializeField]
    private bool ForceSpawn = false;
    
    //this bool can be ticked if you wish to test future and past objects in one game- it will trigger future objects spawning along 
    //past objects when set to true.
    public enum TestingMode {
        Normal,
        SpawnBoth,
        OnlySpawnPast,
        OnlySpawnFuture,
    }

    public TestingMode testingMode;

    public void OnValidate() {
        if (ForceSpawn) {
            if (SceneManager.GetActiveScene().name != "TestingSuite") { Debug.LogError("I refuse to load in an invalid scene"); ForceSpawn = false; return; }
            NetworkManager.Singleton.StartHost();
            forceObjectSpawn();
            ForceSpawn = false;
        }
    }

    private void forceObjectSpawn() {
        if (NetworkManager == null) {
            Debug.LogError("NetworkManager is blerry missing!");
            return;
        }
        Vector3 origin = Vector3.zero;
        Quaternion rotation = Quaternion.Euler(0, 90, 0);
        // Read https://docs-multiplayer.unity3d.com/netcode/current/basics/object-spawning/
        foreach (NetworkPrefab NetworkPrefab in ObjectsToSpawn.PrefabList) {
            var instance = Instantiate(NetworkPrefab.Prefab);
            var networkInstance = instance.GetComponent<NetworkObject>();
            networkInstance.Spawn();
            PastObject pastObject = Instantiate(networkInstance.gameObject.GetComponent<Variables>().PastObjectPrefab, origin, rotation).GetComponent<PastObject>();
            origin.z += 2;
            FutureObject futureObject = Instantiate(networkInstance.gameObject.GetComponent<Variables>().FutureObjectPrefab, origin, rotation).GetComponent<FutureObject>();
            origin.z += 2;
            pastObject.networkObject = networkInstance;
            futureObject.networkObject = networkInstance;
            pastObject.Setup();
            futureObject.Setup();
        }
    }

    public void Awake() {
        Singleton = this;
        DontDestroyOnLoad(gameObject);
        SafeCode = new int[3];
        SafeCode[0] = Random.Range(0,10);
        SafeCode[1] = Random.Range(0,10);
        SafeCode[2] = Random.Range(0,10);
        Debug.Log("Safe code generated: " + SafeCode[0] + SafeCode[1] + SafeCode[2]);
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
                SpawnWitnessPleaseRPC();
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
    public void SpawnWitnessPleaseRPC() {
            foreach (GameObject futureObject in LocalFutureObjects)
            {
                if (!IsServer) Instantiate(futureObject);
            }
        }

    [Rpc(SendTo.ClientsAndHost)]
    public void InstantiatePastObjectRPC(NetworkObjectReference objectReference) {
        // TODO - Add transforms to all Instantiate calls 
        if (testingMode == TestingMode.OnlySpawnFuture) return;
        if (IsServer || testingMode == TestingMode.SpawnBoth || testingMode == TestingMode.OnlySpawnPast) { //Check if you are the past player here
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
        if (!IsServer || testingMode == TestingMode.SpawnBoth || testingMode == TestingMode.OnlySpawnFuture) { //Check if you are the future player here
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