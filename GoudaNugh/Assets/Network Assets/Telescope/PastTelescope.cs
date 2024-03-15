using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PastTelescope : PastObject
{
    public NetworkObject TelescopeVariables;
    public TelescopeVariables vars;
    public GameObject[] lenses;
    private float[] code;
    public int solved;
    // Start is called before the first frame update
    void Start()
    {
        vars = TelescopeVariables.GetComponent<TelescopeVariables>();
        code = new float[] { 245, 165, 190 };
    }

    public void CheckLenses()
    {
       
        for (int i = 0; i < 3; i++)
        {
            float rot = lenses[i].transform.rotation.eulerAngles.z;
            Debug.Log(lenses[1].transform.rotation.eulerAngles.z);
            float answer = rot - code[i];
            if ((answer > -10) && (answer < 10))
            {
                solved++;
                NextCode();
            }
        }
        if (solved > 2)
        {
            Solve();
        }
        solved = 0;
    }

    private void Solve()
    {
        vars.Solve();
    }

    private void NextCode()
    {
        vars.NextCode();
     }
}
