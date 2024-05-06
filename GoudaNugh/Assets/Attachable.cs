using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Attachable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private UnityEvent onAttach;
    public ClueLinker.Links link;
    private BoxCollider box;
    private AttachableCheck attachableBase;
    private XRGrabInteractable grab;

    void Start()
    {
        box = GetComponent<BoxCollider>();
        grab = GetComponent<XRGrabInteractable>();

        grab.selectExited.AddListener(OnSelectExited);
    }

    public void OnSelectExited(SelectExitEventArgs args)
    {
        if (attachableBase != null && attachableBase.iwantthis == link)
        {
            attachableBase.objectReleased();
            onAttach.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        attachableBase = other.gameObject.GetComponent<AttachableCheck>();
        
    }

}
