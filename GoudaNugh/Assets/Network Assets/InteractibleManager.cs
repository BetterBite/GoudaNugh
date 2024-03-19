using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

public class InteractibleManager : NetworkBehaviour {
    public static InteractibleManager Instance { get; private set; }
    public List<GameObject> ObjectsToSpawn;
    // Idea of using a delegate to cut on massive if-else statements was suggested by GitHub Copilot Chat. Thanks GitHub Copilot Chat
    private delegate GameObject InstantiateDelegate(PastObject pastObject, NetworkObject networkObject);
    private readonly Dictionary<Type, InstantiateDelegate> instantiateObject = new Dictionary<Type, InstantiateDelegate> {
        { typeof(PastObject), InstantiatePastObject },
        { typeof(FutureObject), InstantiateFutureObject },
    };


    public void Awake() {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // I love "if's as guards"
    public void CheckSceneEvent(SceneEvent sceneEvent) {
        if (sceneEvent.SceneEventType == SceneEventType.LoadEventCompleted) OnSceneLoad();
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
                NetworkObjectReference objectReference = new NetworkObjectReference(networkInstance);
                InstantiatePastObjectRPC(objectReference);
                InstantiateFutureObjectRPC(objectReference);

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
    

    // Given an ID, find the network object from that ID
    public NetworkObject FindNetworkObject(ulong networkObjectId) {
        NetworkObject networkObject;
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out networkObject)) {
            return networkObject;
        } else {
            return null;
        }
    }
    */

    private static GameObject InstantiatePastObject(PastObject pastObject, NetworkObject networkObject) {
        return null;
    }

    private static GameObject InstantiateFutureObject()

    private static GameObject InstantiatePastTelescope(PastObject pastObject, Variables variables, NetworkObject networkObject) {
        TelescopeVariables telescopeVariables = (TelescopeVariables)variables;
        GameObject Object = Instantiate(telescopeVariables.PastObjectPrefab);
        PastTelescope pastTelescope = Object.GetComponent<PastTelescope>();
        pastTelescope.networkObject = networkObject;
        return Object;
    }

    private static GameObject InstantiateRadio(PastObject pastObject, Variables variables, NetworkObject networkObject) {
        RadioVariables radioVariables = (RadioVariables)variables;
        GameObject Object = Instantiate(radioVariables.PastObjectPrefab);
        PastRadio pastRadio = Object.GetComponent<PastRadio>();
        pastRadio.networkObject = networkObject;
        return Object;
    }

    private static GameObject InstantiateSafe(PastObject pastObject, Variables variables, NetworkObject networkObject) {
        SafeVariables safeVariables = (SafeVariables)variables;
        GameObject Object = Instantiate(safeVariables.PastObjectPrefab);
        PastSafe pastSafe = Object.GetComponent<PastSafe>();
        pastSafe.networkObject = networkObject;
        return Object;
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
            // NetworkObject networkObject = FindNetworkObject(networkobjectid);

            objectReference.TryGet(out NetworkObject networkObject);
            GameObject Object = Instantiate(networkObject.gameObject.GetComponent<Variables>().PastObjectPrefab);
            Object.GetComponent<PastObject>().networkObject = networkObject;
            Object.Setup();
            TransferOwnerServerRPC(objectReference, NetworkManager.Singleton.LocalClientId);
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void InstantiateFutureObjectRPC(NetworkObjectReference objectReference) {

        // TODO - Add transforms to all Instantiate calls 
        if (!IsServer) { //Check if you are the future player here
            objectReference.TryGet(out NetworkObject networkObject);
            GameObject Object = Instantiate(networkObject.gameObject.GetComponent<Variables>().FutureObjectPrefab);
            Object.GetComponent<FutureObject>().networkObject = networkObject;
            Object.Setup();
            TransferOwnerServerRPC(objectReference, NetworkManager.Singleton.LocalClientId);
            TransferOwnerServerRPC(objectReference, NetworkManager.Singleton.LocalClientId);
        }
    }

    [Rpc(SendTo.Server)]
    public void TransferOwnerServerRPC(NetworkObjectReference objectReference, ulong clientID) {
        objectReference.TryGet(out NetworkObject networkObject);
        networkObject.ChangeOwnership(clientID);
    }
}
