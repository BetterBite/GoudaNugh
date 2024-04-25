using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNumbers : MonoBehaviour {
    public GameObject[] numbers;

    void Start() {
        int[] safeCode = InteractibleManager.Singleton.SafeCode;
        for (int i = 0; i < numbers.Length; i++) {
            if (i >= 9) {
                SafeNumber safeNumber = numbers[i].GetComponent<SafeNumber>();
                safeNumber.num = i >= 9 ? safeCode[i - 9] : Random.Range(0, 10);
                safeNumber.content.text = safeNumber.num.ToString();
            }
        }
    }
}
