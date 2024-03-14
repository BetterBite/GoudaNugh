using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FutureTelescope : FutureObject
{
    public GameObject[] images;
    private GameObject variables;
    public GameObject door;
    // Start is called before the first frame update
    void Awake()
    {
        var script = variables.GetComponent<TelescopeVariables>();
        script.isSolved.OnValueChanged += OnSolved;
        script.solvedStatus.OnValueChanged += NextCode;
    }

    private void OnSolved(bool wasSolved, bool isSolved)
    {
        if (!isSolved)
        {
            Open();
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
