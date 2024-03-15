using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRRigReferences : MonoBehaviour
{
    public static VRRigReferences Singleton;
    public Transform root;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public GameObject pastObjects;
    public GameObject futureObjects;
    
    private void Awake() 
    {
        Singleton = this;
    }

    public void SpawnObjects(bool isPast)
    {
        //if (isPast)
        //{
        //    pastObjects.SetActive(true);
        //} else
        //{
        //    futureObjects.SetActive(true);
        //}
    }

    



}
