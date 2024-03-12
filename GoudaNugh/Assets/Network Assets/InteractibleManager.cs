using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class InteractibleManager : NetworkBehaviour {
    public static InteractibleManager Instance { get; private set; }
    public List<GameObject> ObjectsToSpawn;
    private List<WholeObject> WholeObjects { get; } = new List<WholeObject>();

    // Struct that represents the entire object, including the past, network, and future instances
    public class WholeObject {
        // I hate how public is not public in the Java sense if you create a private setter, this feels wrong
        // In any case, the properties below are only accessible with the get, and you cannot change them
        public PastObject pastObject { get; private set; }
        public NetworkObject networkObject { get; private set; }
        public FutureObject futureObject { get; private set; }
        public ulong objectID { get; private set; }

        // Uses the network object ID to set the object ID of the whole object
        public WholeObject(PastObject pastObject, NetworkObject networkObject, FutureObject futureObject) {
            this.pastObject = pastObject;
            this.networkObject = networkObject;
            this.futureObject = futureObject;
            this.objectID = networkObject.NetworkObjectId;
        }
    }

    // Create a whole object and add it to the list (Also returns it!)
    public WholeObject AddWholeObject(PastObject pastObject, NetworkObject networkObject, FutureObject futureObject) {
        WholeObject wholeObject = new WholeObject(pastObject, networkObject, futureObject);
        WholeObjects.Add(wholeObject);
        return wholeObject;
    }

    // Get WholeObject by ID from list (the calling class can use the getters to access the properties)
    public WholeObject GetWholeObjectByID(ulong objectID) {
        foreach (WholeObject wholeObject in WholeObjects) {
            if (wholeObject.objectID == objectID) {
                return wholeObject;
            }
        }
        Debug.LogError("Unable to find WholeObject with ID " + objectID);
        return null;
    }

    public void Awake() {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    // I love "if's as guards"
    private void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        if (SceneManager.GetActiveScene().name == "BetaSceneMain") {
            var networkManager = NetworkManager.Singleton;
            if (networkManager == null) {
                Debug.LogError("NetworkManager is blerry missing!");
                return;
            }
            // Read https://docs-multiplayer.unity3d.com/netcode/current/basics/object-spawning/
            foreach (GameObject Object in ObjectsToSpawn) {
                var instance = Instantiate(Object);
                var networkInstance = instance.GetComponent<NetworkObject>();
                if (networkInstance == null) {
                    Debug.LogError("NetworkObject is blerry missing from " + Object.name);
                    return;
                }
                networkInstance.Spawn();
                var PastObject = networkInstance.GetComponent<Variables>().PastObjectPrefab;
                var FutureObject = networkInstance.GetComponent<Variables>().FutureObjectPrefab;

                InstantiatePastObject(PastObject, networkInstance.NetworkObjectId);
                InstantiateFutureObject(FutureObject);

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

    // Functions that are called before any RPC calls. Only add one if there needs to be some precondition that only InteractibleManager can check for
    public void UpdateFutureObject(ulong objectID) {
        if (GetWholeObjectByID(objectID).networkObject.GetComponent<Variables>().HasMoved.Value) {
            UpdateFutureObjectClientRPC(objectID);
        }
    }

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

    [Rpc(SendTo.ClientsAndHost)]
    public void InstantiatePastObject(GameObject prefab, ulong networkobjectid) {
        //check if you are past player, if not, dont do anything
        //check what object it is, and spawn depending on what you get
        /*
         public NetworkObject FindNetworkObject(ulong networkObjectId)
        {
            NetworkObject networkObject;
            if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out networkObject)) {
                return networkObject;
            } else {
                return null;
            }
        }
        InteractableManager.TransferOwnerServerRPC(networkObject.NetworkObjectId, NetworkManager.Singleton.LocalClientId);
        */
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void InstantiateFutureObject(GameObject prefab) {
    }

    [Rpc(SendTo.NotMe)]
    public void UpdateFutureObjectClientRPC(ulong objectID) {
        if (!IsServer) {
            WholeObject wholeObject = GetWholeObjectByID(objectID);
            if (wholeObject.networkObject != null && wholeObject.futureObject != null) { // This will error for the past player in future implementations since it should not exist
                wholeObject.futureObject.transform.position = wholeObject.networkObject.transform.position;
                wholeObject.futureObject.transform.rotation = wholeObject.networkObject.transform.rotation;
            } else {
                Debug.LogError("NetworkObject or FutureObject not found for object with id" + objectID);
            }
        }
    }

    [Rpc(SendTo.Server)]
    public void TransferOwnerServerRPC(ulong networkObjectId, ulong clientID) {
        FindNetworkObject(networkObjectId).ChangeOwnership(clientID);
    }

    [Rpc(SendTo.Server)]
    public void LogMovementServerRPC(ulong objectId) {
        NetworkObject networkObject = GetWholeObjectByID(objectId).networkObject;
        if (networkObject != null) {
            networkObject.GetComponent<Variables>().HasMoved.Value = true;
        }
    }
}
