using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class FutureGlobe : FutureObject
{
    private GlobeVariables vars;
    public GameObject counter;
    public Transform pastAxis;

    public GameObject lever;
    public GameObject ghostLever;

    //public Transform targetHoriz;
    public GameObject targetRot;
    public override void Setup()
    {
        vars = networkObject.GetComponent<GlobeVariables>();
        vars.rotation.OnValueChanged += UpdateRotation;
        vars.targetRot.OnValueChanged += UpdateTarget;
        vars.globeOn.OnValueChanged += ToggleGlobe;
        vars.pastLeverGrabbed.OnValueChanged += ReceivePastGrab;
    }

    public void ActivateMoon()
    {
        lever.SetActive(true);
    }

    public void OnGrab()
    {
        vars.LeverActivated();
    }

    private void ReceivePastGrab(bool wasGrabbed, bool isGrabbed)
    {
        ghostLever.SetActive(isGrabbed);
    }

    private void ToggleGlobe(bool wasActive, bool isActive)
    {

        counter.gameObject.SetActive(isActive);
        targetRot.SetActive(isActive);
        
    }

    public void FutureMove(Vector3 vec)
    {
        vars.FutureMoveServerRpc(vec, lever.transform.rotation);
    }

    private void UpdateRotation(Vector3 prevVec, Vector3 currVec) 
    {
        counter.transform.rotation = Quaternion.Euler(currVec);
    }

    private void UpdateTarget(Vector3 prevRot, Vector3 rot)
    {
        if (vars.globeOn.Value) 
        {
            ghostLever.transform.rotation = Quaternion.Euler(prevRot);
        }
        targetRot.transform.rotation = Quaternion.Euler(rot);
        //targetVert.rotation = Quaternion.Euler(vars.targetVert.Value);
    }

    private void Update()
    {

    }

}
