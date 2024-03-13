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

    private bool hasMoved = false;
    public XRGrabInteractable grabInteractable;
    private readonly InteractibleManager InstanceManager = InteractibleManager.instance;

    public void Awake() {
        hasMoved = false;
        grabInteractable = GetComponent<XRGrabInteractable>();        
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
    }

    public void OnSelectEntered(SelectEnterEventArgs args) {
        if (!hasMoved) {
            InstanceManager.LogMovementServerRPC(objectID);
            hasMoved = true;
        }
    }
}
