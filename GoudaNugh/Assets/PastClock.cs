using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PastClock : PastObject {
    public TMP_Text[] code;

    public override void Setup() {
        return;
    }
    public void Solve()
    {
        for (int i = 0; i < code.Length; i++)
        {
            code[i].color = Color.green;
        }
    }
}
