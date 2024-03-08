using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class InteractibleManager : NetworkBehaviour {
    // TODO - Switch away from using tags as they have to predefined, try lookup by name when Spawning is implemented?
    public static InteractibleManager instance { get; private set; }
    private List<WholeObject> WholeObjects { get; } = new List<WholeObject>();


    // Struct that represents the entire object, including the past, network, and future instances
    internal class WholeObject {
        private PastObject pastObject;
        private NetworkObject networkObject;
        private FutureObject futureObject;
        private uint objectID { get; set; }


    }

    public void Awake() {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

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

    // Functions that are called before any RPC calls. Only add one if there needs to be some precondition that only InteractibleManager can check for
    public void UpdateFutureObject(string tag) {
        if (FindFutureObjectWithTag(tag).GetComponent<FutureObject>().HasMoved) return;
        UpdateFutureObjectClientRPC(tag);
    }

    /* 
    RPC Calls here. Make sure each has an attribute specifying to whom it is sent to
    See this Unity doc for all valid attributes https://docs.unity3d.com/Packages/com.unity.netcode.gameobjects@1.8/api/Unity.Netcode.SendTo.html
    
    Hint : To send an RPC call to be executed on the other player's machine, use SendTo.NotMe and check within the function if you are not the server
     */

    [Rpc(SendTo.ClientsAndHost)]
    public void UpdateFutureObjectClientRPC(string tag) {
        NetworkObject networkObject = GetNetworkObjectByTag(tag);
        GameObject futureObject = FindFutureObjectWithTag(tag);
        if (networkObject != null && futureObject != null) { // This will error for the past player in future implementations since it should not exist
            futureObject.transform.position = networkObject.transform.position;
            futureObject.transform.rotation = networkObject.transform.rotation;
        } else {
            Debug.LogError("NetworkObject or FutureObject not found for object with tag" + tag);
        }
    }

    [Rpc(SendTo.Server)]
    public void TransferOwnerServerRPC(ulong networkObjectId, ulong clientID) {
        NetworkObject networkObject = FindNetworkObjectById(networkObjectId);
        if (networkObject != null) {
            networkObject.ChangeOwnership(clientID);
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void LogMovementClientRPC(string tag) {
            FindFutureObjectWithTag(tag).GetComponent<FutureObject>().SetHasMoved(true);
    }
}
