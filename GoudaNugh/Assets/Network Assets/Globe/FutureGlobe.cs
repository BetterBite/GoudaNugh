using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class FutureGlobe : FutureObject
{
    private GlobeVariables vars;
    public GameObject counter;

    public GameObject lever;
    public GameObject ghostLever;

    //public Transform targetHoriz;
    public GameObject target;
    public override void Setup()
    {
        vars = networkObject.GetComponent<GlobeVariables>();

        vars.rotation.OnValueChanged += UpdateRotation;
        vars.targetRot.OnValueChanged += UpdateTarget;

        vars.pastLeverGrabbed.OnValueChanged += ReceivePastGrab;

        vars.globeState.OnValueChanged = UpdateGlobe;
    }

    private void UpdateGlobe(GlobeVariables.GlobeStates prevState, GlobeVariables.GlobeStates state)
    {
        switch (state)
        {
            case GlobeVariables.GlobeStates.Activated:
                counter.gameObject.SetActive(true);
                target.SetActive(true);
                break;
            case GlobeVariables.GlobeStates.Solved:
                counter.gameObject.SetActive(false);
                target.SetActive(false);
                lever.SetActive(false);
                break;
            default:
                return;
        }

    }

    public void ActivateMoon()
    {
        lever.SetActive(true);
        if (vars.globeState.Value == GlobeVariables.GlobeStates.Unactivated) vars.globeState.Value = GlobeVariables.GlobeStates.SingleActivated;
        if (vars.globeState.Value == GlobeVariables.GlobeStates.SingleActivated) vars.globeState.Value = GlobeVariables.GlobeStates.Activated;


    }

    public void OnGrab(bool grabbed)
    {
        vars.FutureLeverGrabbedServerRpc(grabbed);
    }

    private void ReceivePastGrab(bool wasGrabbed, bool isGrabbed)
    {
        ghostLever.SetActive(isGrabbed);
    }

    public void FutureMove(Vector3 vec)
    {
        vars.FutureMoveServerRpc(vec, lever.transform.rotation);
    }

    private void UpdateRotation(Vector3 prevVec, Vector3 currVec) 
    {
        ghostLever.transform.rotation = vars.pastRot.Value;
        counter.transform.rotation = Quaternion.Euler(currVec);
    }

    private void UpdateTarget(Vector3 prevRot, Vector3 rot)
    {
        target.transform.rotation = Quaternion.Euler(rot);
        //targetVert.rotation = Quaternion.Euler(vars.targetVert.Value);
    }

    private void Update()
    {

    }

}
