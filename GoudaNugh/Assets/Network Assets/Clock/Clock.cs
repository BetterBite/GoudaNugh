using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class Clock : MonoBehaviour {
    public TMP_Text[] code;

    public void Awake() {
        foreach (var c in code) {
            Assert.IsNotNull(c, "One or more code references in Clock object are missing!");
        }
    }
    public void Solve()
    {
        for (int i = 0; i < code.Length; i++)
        {
            code[i].color = Color.green;
        }
    }
}