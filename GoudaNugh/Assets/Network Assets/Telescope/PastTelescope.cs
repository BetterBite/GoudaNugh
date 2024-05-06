using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using System.Xml.Serialization;
using Meta.WitAi.Requests;

public class PastTelescope : PastObject
{
    public TelescopeVariables vars;
    public Image[] stars;
    private float[] code;
    public int solved;

    public LockableObject door;
    public GameObject telescopeUnlocks;

    public Renderer[] lensRends;
    // Start is called before the first frame update

    public override void Setup() {
        vars = networkObject.GetComponent<TelescopeVariables>();
        vars.solvedStatus.OnValueChanged = NextCode;
        vars.isSolved.OnValueChanged = Solve;
        //code = new float[] { 245, 165, 190 };
        awakenStars(0);
    }

    private void awakenStars(int i)
    {
        
        foreach (var item in stars)
        {
            if(i < 3)
            {
                item.enabled = false;
                stars[i].enabled = true;
            }

        }
    }

    private void Solve(bool wasSolved, bool solved)
    {
        if (solved)
        {
            door.Unlock();
            telescopeUnlocks.SetActive(true);

        }
    }

    private void NextCode(int prevStatus, int status) {
        lensRends[prevStatus].material.color = Color.red;
        if (status > 2) { return; }
        awakenStars(status);
        
    }
}
