using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SafeNumber : MonoBehaviour {
    public TMP_Text content;
    public int num;
    public GameObject safeCode;

    void Start() {
        num = Random.Range(0, 10);
        content.text = num.ToString();
    }
    public void Poke() {
        num = (num + 1) % 10;
        content.text = num.ToString();
        safeCode.GetComponent<SafeCode>().CheckCode();
    }
}
