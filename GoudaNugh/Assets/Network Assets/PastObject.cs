using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PastObject : MonoBehaviour
{
    public XRGrabInteractable grabInteractable;
    private NetworkObject networkObject;
    private readonly InteractibleManager InstanceManager = InteractibleManager.instance;

    private void Awake() {
        grabInteractable = GetComponent<XRGrabInteractable>();
        networkObject = InstanceManager.GetNetworkObjectByTag(gameObject.tag);

        if (networkObject == null) {
            Debug.LogError("NetworkObject not found for object with tag" + gameObject.tag);
        } else {
            // Quick hack to transfer ownership to the local client just incase the players are mixed up
            // Also future proof
            InteractibleManager.instance.TransferOwnerServerRPC(networkObject.NetworkObjectId, NetworkManager.Singleton.LocalClientId);
        }
        grabInteractable.selectExited.AddListener(OnSelectExited);
        // TODO - Have PastObject register itself with InstanceManager and have InstanceManager find related FutureObject and NetworkObject at start
    }

    // Update NetworkObject transform on select exit
    // This only works assuming that the transform of the network object represents the transform of the future object, assuming it wasn't moved
    // Only works with a one room representation
    private void OnSelectExited(SelectExitEventArgs args) {
        networkObject.transform.position = transform.position;
        networkObject.transform.rotation = transform.rotation;
        InstanceManager.UpdateFutureObject(gameObject.tag);
    }
}
