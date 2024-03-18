using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class InteractibleManager : NetworkBehaviour {
    public static InteractibleManager Instance { get; private set; }
    public List<GameObject> ObjectsToSpawn;

    public void Awake() {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        //SceneManager.sceneLoaded += OnSceneLoad;
       
    }

    // I love "if's as guards"

    public void CheckSceneEvent(SceneEvent sceneEvent)
    {
        if (sceneEvent.SceneEventType == SceneEventType.LoadComplete) {
            OnSceneLoad();
        }
    }

    public void OnSceneLoad() {
        //if (SceneManager.GetActiveScene().name == "BetaSceneNetworkTest") {
        if (IsServer) { 
            var networkManager = NetworkManager.Singleton;
            Debug.Log("Loading Objects");
            if (networkManager == null) {
                Debug.LogError("NetworkManager is blerry missing!");
                return;
            }
            // Read https://docs-multiplayer.unity3d.com/netcode/current/basics/object-spawning/
            foreach (GameObject NetworkObject in ObjectsToSpawn) {
                Debug.Log("Spawning: " + NetworkObject.name);
                var instance = Instantiate(NetworkObject);
                var networkInstance = instance.GetComponent<NetworkObject>();
                if (networkInstance == null) {
                    Debug.LogError("NetworkObject is blerry missing for " + NetworkObject.name);
                    return;
                }
                networkInstance.Spawn();
                InstantiatePastObjectRPC(networkInstance.NetworkObjectId);
                InstantiateFutureObjectRPC(networkInstance.NetworkObjectId);

            }
        }
    }
    /* DEPRECATED
    public NetworkObject GetNetworkObjectByTag(string tag) {
        NetworkObject[] networkObjects = GetComponentsInChildren<NetworkObject>();
        foreach (NetworkObject networkObject in networkObjects) {
            if (networkObject.gameObject.CompareTag(tag)) {
                return networkObject;
            }
        }
        Debug.LogError("Unable to find NetworkObject with tag " + tag);
        return null;
    }

    // This sucks. Genuinely. But it's the only way to find the future object with the tag for now
    public GameObject FindFutureObjectWithTag(string tag) {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in taggedObjects) {
            FutureObject futureObject = obj.GetComponent<FutureObject>();
            if (futureObject != null) {
                return obj;
            }
        }
        Debug.LogError("Unable to find FutureObject with tag " + tag);
        return null;
    }

    // ty copilot
    public NetworkObject FindNetworkObjectById(ulong networkObjectId) {
        NetworkObject[] networkObjects = GetComponentsInChildren<NetworkObject>();
        foreach (NetworkObject networkObject in networkObjects) {
            if (networkObject.NetworkObjectId == networkObjectId) {
                return networkObject;
            }
        }
        Debug.LogError("Unable to find NetworkObject with id " + networkObjectId);
        return null;
    }
    */

    // Given an ID, find the network object from that ID
    public NetworkObject FindNetworkObject(ulong networkObjectId) {
        NetworkObject networkObject;
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out networkObject)) {
            return networkObject;
        } else {
            return null;
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
    public void InstantiatePastObjectRPC(ulong networkobjectid) {
        // TODO - Add transforms to all Instantiate calls 
        if (IsServer) { //Check if you are the past player here
            NetworkObject networkObject = FindNetworkObject(networkobjectid);
            Variables variables = networkObject.gameObject.GetComponent<Variables>();
            if (variables is TelescopeVariables) {
                TelescopeVariables telescopeVariables = (TelescopeVariables)variables;
                GameObject Object = Instantiate(telescopeVariables.PastObjectPrefab);
                PastTelescope pastTelescope = Object.GetComponent<PastTelescope>();
                pastTelescope.networkObject = networkObject;
            } else if (variables is RadioVariables) {
                RadioVariables radioVariables = (RadioVariables)variables;
                GameObject Object = Instantiate(radioVariables.PastObjectPrefab);
                PastRadio pastRadio = Object.GetComponent<PastRadio>();
                pastRadio.networkObject = networkObject;
            } else if (variables is SafeVariables) {
                SafeVariables safeVariables = (SafeVariables)variables;
                GameObject Object = Instantiate(safeVariables.PastObjectPrefab);
                PastSafe pastSafe = Object.GetComponent<PastSafe>();
                pastSafe.networkObject = networkObject;
            } else {
                Debug.LogError("Could not identify the type of object, real type is " + variables.GetType());
            }
            TransferOwnerServerRPC(networkobjectid, NetworkManager.Singleton.LocalClientId);
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void InstantiateFutureObjectRPC(ulong networkobjectid) {
        // TODO - Add transforms to all Instantiate calls 
        if (!IsServer) { //Check if you are the future player here
            NetworkObject networkObject = FindNetworkObject(networkobjectid);
            Variables variables = networkObject.gameObject.GetComponent<Variables>();
            if (variables is TelescopeVariables) {
                TelescopeVariables telescopeVariables = (TelescopeVariables)variables;
                GameObject Object = Instantiate(telescopeVariables.FutureObjectPrefab);
                FutureTelescope futureTelescope = Object.GetComponent<FutureTelescope>();
                futureTelescope.networkObject = networkObject;
            } else if (variables is RadioVariables) {
                RadioVariables radioVariables = (RadioVariables)variables;
                GameObject Object = Instantiate(radioVariables.FutureObjectPrefab);
                FutureRadio futureRadio = Object.GetComponent<FutureRadio>();
                futureRadio.networkObject = networkObject;
            } else if (variables is SafeVariables) {
                SafeVariables safeVariables = (SafeVariables)variables;
                GameObject Object = Instantiate(safeVariables.FutureObjectPrefab);
                FutureSafe futureSafe = Object.GetComponent<FutureSafe>();
                futureSafe.networkObject = networkObject;
            } else {
                Debug.LogError("Could not identify the type of object, real type is " + variables.GetType());
            }
            TransferOwnerServerRPC(networkobjectid, NetworkManager.Singleton.LocalClientId);
        }
    }

    [Rpc(SendTo.Server)]
    public void TransferOwnerServerRPC(ulong networkObjectId, ulong clientID) {
        FindNetworkObject(networkObjectId).ChangeOwnership(clientID);
    }
}
