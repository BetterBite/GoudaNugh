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

    public Renderer leftHandRenderer;
    public Renderer rightHandRenderer;


    public Material pastMaterial;
    public Material futureMaterial;
    private void Awake() 
    {
        Singleton = this;
    }

    public void ChangeGloves (bool isPast)
    {
        if (isPast)
        {
            leftHandRenderer.material = pastMaterial;
            rightHandRenderer.material = pastMaterial;
        } else
        {
            rightHandRenderer.material = futureMaterial;
            leftHandRenderer.material = futureMaterial;
        }
    }

    



}
