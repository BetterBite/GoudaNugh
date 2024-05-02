using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PastGlobe : PastObject
{
    private GlobeVariables vars;
    public GameObject counter;
    public GameObject targetRot;

    public GameObject lever;
    public GameObject ghostLever;
    public override void Setup()
    {
        vars = networkObject.GetComponent<GlobeVariables>();
        vars.rotation.OnValueChanged += UpdateRotation;
        vars.rotation.Value = counter.transform.rotation.eulerAngles;
        vars.globeOn.OnValueChanged = ToggleGlobe;
        vars.targetRot.OnValueChanged = UpdateTarget;
        vars.futureLeverGrabbed.OnValueChanged = ReceiveFutureGrab;
        vars.globeOn.Value = true;
    }

    private void ToggleGlobe(bool wasActive, bool isActive)
    {

        counter.gameObject.SetActive(isActive);
        targetRot.SetActive(isActive);

    }

    private void ReceiveFutureGrab(bool wasGrabbed, bool isGrabbed)
    {
        ghostLever.SetActive(isGrabbed);
    }

    public void PastMove(Vector3 vector)
    {
        vars.PastMove(vector, lever.transform.rotation);
    }

    public void UpdateRotation(Vector3 prevVec, Vector3 currVec)
    {
        ghostLever.transform.rotation = vars.futureRot.Value;
        counter.transform.rotation = Quaternion.Euler(currVec);
    }

    private void UpdateTarget(Vector3 prevRot, Vector3 rot)
    {
        
        targetRot.transform.rotation = Quaternion.Euler(rot);
        //targetVert.rotation = Quaternion.Euler(vars.targetVert.Value);
    }


}
