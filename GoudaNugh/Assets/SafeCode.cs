using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class SafeCode : MonoBehaviour {
    public int[] code = { 0, 0, 0};
    public TMP_Text[] content;
    public Hinge hinge;

    void Start() {
        code[0] = Random.Range(0, 10);
        code[1] = Random.Range(0, 10);
        code[2] = Random.Range(0, 10);
        Debug.Log("Code generated: " + code[0] + code[1] + code[2]);
    }

    // Returns true if the code is correct
    public bool CheckCode() {
        int count = 0;
        for (int i = 0; i < 3; i++) {
            if (code[i] == Int32.Parse(content[i].text)) {
                count++;
            } 
        }
        if (count>2) {
            Debug.Log("correct code");
            hinge.open();
            return true;
        }
        return false;
    }
}
