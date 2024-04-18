using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PastGlobe : PastObject
{
    private GlobeVariables vars;
    public CounterMove counter;
    public Transform targetRot;
    public override void Setup()
    {
        vars = networkObject.GetComponent<GlobeVariables>();
        vars.rotation.OnValueChanged += UpdateRotation;
        vars.rotation.Value = counter.transform.rotation.eulerAngles;

        vars.targetRot.OnValueChanged = UpdateTarget;
    }

    public void PastMove(Vector3 vector)
    {
        vars.PastMove(vector);
    }

    public void UpdateRotation(Vector3 prevVec, Vector3 currVec)
    {
        counter.transform.rotation = Quaternion.Euler(currVec);
    }

    private void UpdateTarget(Vector3 prevRot, Vector3 rot)
    {
        targetRot.rotation = Quaternion.Euler(rot);
        //targetVert.rotation = Quaternion.Euler(vars.targetVert.Value);
    }


}
