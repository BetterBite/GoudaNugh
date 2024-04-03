using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PastDrawers : PastObject
{
    public DrawersVariables drawersVars;
    // Start is called before the first frame update
    public override void Setup()
    {
        drawersVars = networkObject.GetComponent<DrawersVariables>();
    }

    public void UnlockforNetwork()
    {
        drawersVars.ToggleLock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
