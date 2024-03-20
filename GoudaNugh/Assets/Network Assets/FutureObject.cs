using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.Netcode;

public abstract class FutureObject : MonoBehaviour
{
    public NetworkObject networkObject;
    [SerializeField]
    private bool hasMoved = false;
    //public XRGrabInteractable grabInteractable;
    private readonly InteractibleManager InstanceManager = InteractibleManager.Instance;

    public abstract void Setup();

    /* 
     * Example code of how to setup the listener for movement
    public void Awake() {
        hasMoved = false;
        grabInteractable = GetComponent<XRGrabInteractable>();        
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
    } 

    public void Setup() {
        variables = networkObject.gameObject.GetComponent<Variables>();
        variables.HasMoved.OnValueChanged += OnHasMovedChanged;
    }

/    public void OnHasMovedChanged(bool previousValue, bool newValue) {
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
    */
}
