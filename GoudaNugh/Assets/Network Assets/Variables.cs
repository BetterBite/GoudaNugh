using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Variables : NetworkBehaviour
{
    NetworkVariable<bool> hasMoved = new NetworkVariable<bool>(false);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
