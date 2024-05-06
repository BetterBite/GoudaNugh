using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleHint : Hint
{
    public GameObject subtitle;
    private Transform t;
    public Vector3 offset;
    [TextArea(4,10)]
    public string subtitleText = "Hello";
    public override void DisplayHint()
    {
        var sub = Instantiate(subtitle, transform.position + offset, Quaternion.identity, transform);
        sub.GetComponent<TMP_Text>().text = subtitleText;
        return;
    }

    // Start is called before the first frame update
    void Start()
    {
        t = gameObject.transform;
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
