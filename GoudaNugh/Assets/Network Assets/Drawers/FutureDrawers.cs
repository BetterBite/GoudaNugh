using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureDrawers : FutureObject
{
    public DrawersVariables drawersVars;
    public GameObject lockedDrawer;
    public override void Setup()
    {
       drawersVars = networkObject.GetComponent<DrawersVariables>();
        drawersVars.isLocked.OnValueChanged += Unlock;
    }

    private void Unlock(bool wasLocked, bool isLocked)
    {
        //GetComponentInChildren<LockableObject>().Unlock();
        var script = lockedDrawer.GetComponent<LockableObject>();
        script.Unlock();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}