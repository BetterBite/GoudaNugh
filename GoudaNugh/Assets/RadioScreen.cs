using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RadioScreen : MonoBehaviour
{
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void enterNumber(string number)
    {
        text.text = text.text + number;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
