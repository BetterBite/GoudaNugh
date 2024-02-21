using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PastObject : MonoBehaviour
{
    public XRGrabInteractable grabInteractable;
    private NetworkObject networkObject;
    private InstanceManager InstanceManager;

    private void Awake() {
        InstanceManager = InstanceManager.instance;
        grabInteractable = GetComponent<XRGrabInteractable>();
        networkObject = InstanceManager.GetNetworkObjectByTag(gameObject.tag);

        if (networkObject == null) {
            Debug.LogError("NetworkObject not found for object with tag" + gameObject.tag);
        } else {
            // Quick hack to transfer ownership to the local client just incase the players are mixed up
            // Also future proof
            InstanceManager.instance.TransferOwnerToClient(networkObject, NetworkManager.Singleton.LocalClientId);
        }
        grabInteractable.selectExited.AddListener(OnSelectExited);
        // TODO - Have PastObject register itself with InstanceManager and have InstanceManager find related FutureObject and NetworkObject at start
    }

    // Update NetworkObject transform on select exit
    // This only works assuming that the transform of the network object represents the transform of the future object, assuming it wasn't moved
    // Turns out transform coords are relative to the parent so no need to calculate offset
    private void OnSelectExited(SelectExitEventArgs args) {
        networkObject.transform.position = transform.position;
        networkObject.transform.rotation = transform.rotation;
        InstanceManager.UpdateFutureObject(gameObject.tag);
    }
}
