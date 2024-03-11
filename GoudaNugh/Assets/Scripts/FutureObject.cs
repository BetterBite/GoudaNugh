using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.Netcode;

public class FutureObject : MonoBehaviour
{
    private bool hasMoved = false;
    public XRGrabInteractable grabInteractable;
    private InstanceManager InstanceManager;

    public bool HasMoved {
        get {
            return hasMoved;
        }
    }

    public void SetHasMoved(bool value) {
        hasMoved = value;
    }

    public void Awake() {
        hasMoved = false;
        InstanceManager = GetComponent<InstanceManager>();
        grabInteractable = GetComponent<XRGrabInteractable>();        
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
    }

    public void OnSelectEntered(SelectEnterEventArgs args) {
        if (!hasMoved) {
            InstanceManager.LogMovement(gameObject.tag);
            hasMoved = true;
        }
    }
}