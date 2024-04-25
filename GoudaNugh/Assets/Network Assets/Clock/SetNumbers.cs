using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetNumbers : MonoBehaviour {
    public TMP_Text[] numbers;

    void Start() {
        int[] safeCode = InteractibleManager.Singleton.SafeCode;
        for (int i = 0; i < numbers.Length; i++) {
            if (i >= 9) {
                // Throws a null reference exception for no reason?? Still works and no actual errors can be identified
                numbers[i].text = i >= 9 ? safeCode[i - 9].ToString() : Random.Range(0, 10).ToString();
            }
        }
    }
}
