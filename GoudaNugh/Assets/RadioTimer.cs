using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        Fill[0].fillAmount = time / Max;
    }
}
