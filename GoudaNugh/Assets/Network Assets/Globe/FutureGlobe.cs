using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class FutureGlobe : FutureObject
{
    private GlobeVariables vars;
    public CounterMove counter;
    public Transform pastAxis;

    //public Transform targetHoriz;
    public Transform targetRot;
    private bool goingDown;
    private float timer = 0;
    public override void Setup()
    {
        vars = networkObject.GetComponent<GlobeVariables>();
        vars.rotation.OnValueChanged += UpdateRotation;
        vars.targetRot.OnValueChanged += UpdateTarget;
    }

    public void ActivateMoon()
    {

    }

    public void FutureMove(Vector3 vec)
    {
        vars.FutureMoveServerRpc(vec);
    }

    private void UpdateRotation(Vector3 prevVec, Vector3 currVec) 
    {
        counter.transform.rotation = Quaternion.Euler(currVec);
    }

    private void UpdateTarget(Vector3 prevRot, Vector3 rot)
    {
        targetRot.rotation = Quaternion.Euler(rot);
        //targetVert.rotation = Quaternion.Euler(vars.targetVert.Value);
    }

    private void Update()
    {

    }

}
