using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FutureTelescope : FutureObject
{
    public GameObject[] images;
    public TelescopeVariables telescopeVariables;
    public GameObject door;

    public override void Setup() {
        telescopeVariables = networkObject.GetComponent<TelescopeVariables>();
        telescopeVariables.isSolved.OnValueChanged += OnSolved;
        telescopeVariables.solvedStatus.OnValueChanged += NextCode;
    }

    private void OnSolved(bool wasSolved, bool isSolved) {
        if (isSolved) {
            // Open();
            Debug.Log("Solved on client side!");
        }
    }

    private void NextCode(int statusBefore, int statusNow) {
        if(statusNow > statusBefore)
        {
            for (int i = 0; i < 3; i++)
            {
                images[i].SetActive(false);
            }
            images[statusNow].SetActive(true);
        }
    }
    private void Open() {
        door.SetActive(false);
    }
}
