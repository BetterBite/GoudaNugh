using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hinge : MonoBehaviour
{
    public Transform transform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void open()
    {
        transform.Rotate(new Vector3(0, 90, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
