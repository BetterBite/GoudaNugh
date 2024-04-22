using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour
{

    public float spawnRate = 2;
    private float timer;
    public GameObject debris;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        } else
        {
            Instantiate(debris, transform.position + new Vector3(0,1,0), Quaternion.identity);
            timer = 0;
        }
    }
}
