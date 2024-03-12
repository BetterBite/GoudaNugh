using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PastObject : MonoBehaviour
{
    // Same as network object ID
    [SerializeField]
    private ulong objectID;
    // Cached reference to the network object
    private NetworkObject networkObject;
    private XRGrabInteractable grabInteractable;
    private readonly InteractibleManager InteractableManager = InteractibleManager.instance;

    // Note: objectID field is not yet initialized (probably) when Awakening so networkObjectID is used to transfer ownership
    private void Awake() {
        grabInteractable = GetComponent<XRGrabInteractable>();
        networkObject = InteractableManager.GetWholeObjectByID(objectID).networkObject;

        if (networkObject = null) {
            Debug.LogError("NetworkObject not found for this object");
        } else {
            InteractableManager.TransferOwnerServerRPC(networkObject.NetworkObjectId, NetworkManager.Singleton.LocalClientId);
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
        InteractableManager.UpdateFutureObject(objectID);
    }
}
