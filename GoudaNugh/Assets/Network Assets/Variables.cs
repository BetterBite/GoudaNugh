using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Variables : NetworkBehaviour
{
    // NetworkVariables for the gameObject
    [SerializeField]
    private NetworkVariable<bool> _hasMoved = new NetworkVariable<bool>(false);
    
    // VS generated encapsulation for _hasMoved, copy paste this for any other NetworkVariable you add
    public NetworkVariable<bool> HasMoved { get => _hasMoved; set => _hasMoved = value; }

    // If you want either the FutureObject or PastObject to do something when these variables change, you can add an event here using OnValueChanged

}
