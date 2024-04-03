using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DrawersVariables : Variables
{

    public NetworkVariable<bool> isLocked = new NetworkVariable<bool>(true);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ToggleLock()
    {
        Debug.Log("toggled");
        isLocked.Value = !isLocked.Value;  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
