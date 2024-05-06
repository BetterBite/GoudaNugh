using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FutureTelescope : FutureObject
{
    public TelescopeVariables vars;
    public GameObject telescopeUnlocks;
    public LockableObject door;
    public int[] angles = { 235, 170, 185 };
    public LensScript[] lenses;

    public override void Setup() {
        vars = networkObject.GetComponent<TelescopeVariables>();
        vars.isSolved.OnValueChanged += OnSolved;


        foreach (var s in lenses) { 
            s.rotatable = false;
        }
        lenses[vars.solvedStatus.Value].rotatable = true;

        //telescopeVariables.solvedStatus.OnValueChanged += NextCode;
    }

    private void OnSolved(bool wasSolved, bool isSolved) {
        if (isSolved) {
            Open();
            Debug.Log("Solved on client side!");
        }
    }

    public void NextCode() {
        vars.NextCodeServerRpc();
        if (vars.solvedStatus.Value < 3)
        {

            Debug.Log("solvedStatus:" + vars.solvedStatus.Value);

            lenses[vars.solvedStatus.Value].rotatable = true;

        } else
        {
            
        }
        //if(statusNow > statusBefore)
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        images[i].SetActive(false);
        //    }
        //    images[statusNow].SetActive(true);
        //}
    }
    private void Open() {
        door.Unlock();
        telescopeUnlocks.SetActive(true);
    }
}
