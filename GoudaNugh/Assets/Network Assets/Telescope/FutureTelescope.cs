using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FutureTelescope : FutureObject
{
    public GameObject[] images;
    public TelescopeVariables variables;
    public GameObject door;
    // Start is called before the first frame update

    public override void Setup()
    {
        //base.Setup();
        variables.isSolved.OnValueChanged += OnSolved;
        variables.solvedStatus.OnValueChanged += NextCode;

    }

    private void OnSolved(bool wasSolved, bool isSolved)
    {
        if (!isSolved)
        {
            Open();
            Debug.Log("Solved on client side!");
        }
    }

    private void NextCode(int statusBefore, int statusNow)
    {
        if(statusNow > statusBefore)
        {
            for (int i = 0; i < 3; i++)
            {
                images[i].SetActive(false);
            }
            images[statusNow].SetActive(true);
        }

    }



    private void Open()
    {
        door.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
