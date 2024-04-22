using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering;

public class Debris : NetworkBehaviour
{
    // Start is called before the first frame update
    private bool IsPast;
    public MeshRenderer mr;
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mr.materials[0].color = Color.white;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, 50, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
