using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterMove : MonoBehaviour
{
    // Start is called before the first frame update
    public PastGlobeLever lever;
    void Start()
    {
        
    }

    public void rotate(Vector3 vec)
    {
        Debug.Log("rotating");
        transform.Rotate(vec);
    }

    // Update is called once per frame
    void Update()
    {
        //if (lever.grabbed)
        //{
        //    Debug.Log("rotating");
        //    transform.Rotate(new Vector3(0, 0, lever.velocity));
        //}
    }
}
