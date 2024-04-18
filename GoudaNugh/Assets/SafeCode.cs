using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class SafeCode : MonoBehaviour
{
    bool Locked = true;
    public int[] code = { Random.Range(0,10), Random.Range(0,10), Random.Range(0,10)};
    public TMP_Text[] content;
    public GameObject hinge;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Code generated: " + code[0] + code[1] + code[2]);
    }

    public void CheckCode()
    {
        int count = 0;
        for (int i = 0; i < 3; i++)
        {
            if (code[i] == Int32.Parse(content[i].text))
            {
                count++;
            } 
        }
        if (count>2)
        {
            Debug.Log("correct code");
            hinge.GetComponent<Hinge>().open();
        }
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
