using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.Netcode;

public class FutureObject : MonoBehaviour
{
    // Same as network object ID
    [SerializeField]
    private uint objectID;

    public NetworkObject networkObject;
    private bool hasMoved = false;
    public XRGrabInteractable grabInteractable;
    private readonly InteractibleManager InstanceManager = InteractibleManager.Instance;

    public void Awake() {
        hasMoved = false;
        grabInteractable = GetComponent<XRGrabInteractable>();        
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
    }

    public void OnSelectEntered(SelectEnterEventArgs args) {
        if (!hasMoved) {
            Debug.Log("Future object has been moved");
            hasMoved = true;

        }
    }
}
