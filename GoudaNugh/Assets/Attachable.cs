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

    void Start()
    {
        box = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        attachableBase = other.gameObject.GetComponent<AttachableCheck>();
        
    }


    public void OnSelectExited()
    {
        if (attachableBase != null && attachableBase.iwantthis == link) 
        {
            attachableBase.objectReleased();
            onAttach.Invoke();
        }
    }
}
