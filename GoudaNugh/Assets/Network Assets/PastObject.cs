using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class PastObject : MonoBehaviour
{
    // Same as network object ID
    [SerializeField]
    private ulong objectID;
    // Cached reference to the network object
    public NetworkObject networkObject;
    private XRGrabInteractable grabInteractable;
    private readonly InteractibleManager InteractableManager = InteractibleManager.Instance;

    public virtual void Setup()
    {

    }
    // Note: objectID field is not yet initialized (probably) when Awakening so networkObjectID is used to transfer ownership
    private void Awake() {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    // Update NetworkObject transform on select exit
    // This only works assuming that the transform of the network object represents the transform of the future object, assuming it wasn't moved
    // Only works with a one room representation
    private void OnSelectExited(SelectExitEventArgs args) {
        networkObject.transform.position = transform.position;
        networkObject.transform.rotation = transform.rotation;
    }
}
