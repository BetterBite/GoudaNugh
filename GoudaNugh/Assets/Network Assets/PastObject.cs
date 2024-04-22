using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class PastObject : MonoBehaviour
{
    // Cached reference to the network object
    public NetworkObject networkObject;
    //private XRGrabInteractable grabInteractable;
    private readonly InteractibleManager InteractableManager = InteractibleManager.Singleton;

    public abstract void Setup();
    /*
     * Example of how to setup a listener for listening to movement
    public virtual void Setup() {
        variables = networkObject.gameObject.GetComponent<Variables>();
    }

    private void Awake() {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectExited(SelectExitEventArgs args) {
        networkObject.transform.position = transform.position;
        networkObject.transform.rotation = transform.rotation;
    }
    */
}
