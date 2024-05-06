using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

public class SetNumbers : MonoBehaviour {
    public TMP_Text[] numbers;

    void Start() {
        int[] safeCode = Spawner.Singleton.SafeCode;
        foreach (var number in numbers) {
            Assert.IsNotNull(number, "One or more of the numbers for the alarm clock is not assigned!");
        }
        for (int i = 0; i < numbers.Length; i++) {
            numbers[i].text = i >= 9 ? safeCode[i-9].ToString() : Random.Range(0,10).ToString();
        }
    }
}
