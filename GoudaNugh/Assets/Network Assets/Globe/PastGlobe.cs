using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PastGlobe : PastObject
{

    private bool localGlobeActivated = false;
    public Transform counterTrans;
    public Transform targetTrans;

    private GlobeVariables vars;
    public GameObject counter;
    public GameObject target;

    public GameObject lever;
    public GameObject ghostLever;


    public Renderer earthRend;
    public Material[] transparentMaterials;
    public override void Setup()
    {
        vars = networkObject.GetComponent<GlobeVariables>();

        vars.rotation.OnValueChanged += UpdateRotation;
        
        vars.targetRot.OnValueChanged = UpdateTarget;
        vars.futureLeverGrabbed.OnValueChanged = ReceiveFutureGrab;

        vars.rotation.Value = counter.transform.rotation.eulerAngles;

        vars.globeState.OnValueChanged = UpdateGlobe;
    }

    private void UpdateGlobe(GlobeVariables.GlobeStates prevState, GlobeVariables.GlobeStates state)
    {
        switch (state)
        {
            case GlobeVariables.GlobeStates.Activated:
                counter.gameObject.SetActive(true);
                target.SetActive(true);
                localGlobeActivated = true;
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

    public void OnGrab(bool grabbed)
    {
        vars.PastLeverGrabbed(grabbed);
    }

    public void ActivateSun()
    {
        lever.SetActive(true);
        if (vars.globeState.Value == GlobeVariables.GlobeStates.Unactivated) vars.globeState.Value = GlobeVariables.GlobeStates.SingleActivated;
        if (vars.globeState.Value == GlobeVariables.GlobeStates.SingleActivated) vars.globeState.Value = GlobeVariables.GlobeStates.Activated;


    }

    public void StartGlobe()
    {
        earthRend.materials[0] = transparentMaterials[0]; 
            //= transparentMaterials[0];
        earthRend.materials[1] = transparentMaterials[1];
        Debug.Log(earthRend.materials[0]);
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
        
        target.transform.rotation = Quaternion.Euler(rot);
        //targetVert.rotation = Quaternion.Euler(vars.targetVert.Value);
    }

    private void Update()
    {
        //Debug.Log(Vector3.Distance(counterTrans.position, targetTrans.position));

        if (Vector3.Distance(counterTrans.position, targetTrans.position) < 0.05)
        {
            //Debug.Log("Solved Globe!");
            //trigger solved behaviour
        }
    }

}
