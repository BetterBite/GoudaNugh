using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PastTelescope : PastObject
{
    public TelescopeVariables telescopeVariables;
    public GameObject[] lenses;
    private float[] code;
    public int solved;
    // Start is called before the first frame update

    public override void Setup()
    {
        base.Setup();
        telescopeVariables = (TelescopeVariables)variables;
        code = new float[] { 245, 165, 190 };
    }

    public void CheckLenses()
    {
       
        //for (int i = 0; i < 3; i++)
        //{
        //    float rot = lenses[i].transform.rotation.eulerAngles.z;
        //    Debug.Log(lenses[1].transform.rotation.eulerAngles.z);
        //    float answer = rot - code[i];
        //    if ((answer > -10) && (answer < 10))
        //    {
        //        solved++;
        //        NextCode();
        //    }
        //}
        //if (solved > 2)
        //{
        //    Solve();
        //}
        //solved = 0;
        //Solve();
    }

    public void Solve()
    {
        telescopeVariables.Solve();
    }

    private void NextCode()
    {
        telescopeVariables.NextCode();
     }
}
