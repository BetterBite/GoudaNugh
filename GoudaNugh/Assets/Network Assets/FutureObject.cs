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

    // Setup any listeners to the network variables
    public virtual void Setup() {
        networkObject.gameObject.GetComponent<Variables>().HasMoved.OnValueChanged += OnHasMovedChanged;
    }

    public void OnHasMovedChanged(bool previousValue, bool newValue) {
        if (hasMoved) {
            return;
        }
        transform.position = networkObject.transform.position;
        transform.rotation = networkObject.transform.rotation;
    }
    public void OnSelectEntered(SelectEnterEventArgs args) {
        if (!hasMoved) {
            Debug.Log("Future object has been moved");
            hasMoved = true;
        }
    }
}
