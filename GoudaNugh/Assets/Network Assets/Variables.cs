using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Variables : NetworkBehaviour
{
    // Prefabs for the past and future objects, make sure to set these in the inspector
    // These are NOT to be set at runtime. Each asset needs to have a prefab for the past and future object and it needs to be assigned here
    [SerializeField]
    private readonly GameObject pastObjectPrefab;
    [SerializeField]
    private readonly GameObject futureObjectPrefab;

    public GameObject PastObjectPrefab => pastObjectPrefab;
    public GameObject FutureObjectPrefab => futureObjectPrefab;

    // NetworkVariables for the gameObject
    [SerializeField]
    private NetworkVariable<bool> _hasMoved = new NetworkVariable<bool>(false);

    // Check if the prefabs are set. Important!
    public void Awake() {
        if (pastObjectPrefab == null) {
            Debug.LogError("PastObject prefab not set for this GameObject!");
        }
        if (futureObjectPrefab == null) {
            Debug.LogError("FutureObject prefab not set for this GameObject!");
        }
    }


    // VS generated encapsulation for _hasMoved, copy paste this for any other NetworkVariable you add
    public NetworkVariable<bool> HasMoved { get => _hasMoved; set => _hasMoved = value; }

    // If you want either the FutureObject or PastObject to do something when these variables change, you can add an event here using OnValueChanged

}
