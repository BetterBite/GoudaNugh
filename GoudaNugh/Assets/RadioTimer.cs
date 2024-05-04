using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RadioTimer : MonoBehaviour
{
    public float time;
    public Image[] Fill;
   
    public float Max;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    public void ResetTimers()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        Fill[0].fillAmount = time / Max;
    }
}
