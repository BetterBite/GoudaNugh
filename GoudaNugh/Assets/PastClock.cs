using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PastClock : PastObject
{

    public TMP_Text[] code;
    public override void Setup()
    {
        InteractibleManager IManager = InteractibleManager.Singleton;
        for (int i = 0; i < code.Length; i++)
        {
            code[i].text = IManager.SafeCode[i].ToString();
        }
        
    }


    private void Start()
    {
        InteractibleManager IManager = InteractibleManager.Singleton;
        for (int i = 0; i < code.Length; i++)
        {
            code[i].text = IManager.SafeCode[i].ToString();
        }
    }

    public void Solve()
    {
        for (int i = 0; i < code.Length; i++)
        {
            code[i].color = Color.green;
        }
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
}
