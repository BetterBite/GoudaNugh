using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastCabinet : PastObject
{
    public CabinetVariables vars;
    public override void Setup()
    {
        vars = networkObject.GetComponent<CabinetVariables>();
    }

    public void Unlock()
    {
        vars.Unlock();
    }
}
