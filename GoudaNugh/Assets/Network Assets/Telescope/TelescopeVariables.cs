using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TelescopeVariables : NetworkBehaviour
{

    public NetworkVariable<bool> isSolved = new NetworkVariable<bool>(false);
    public NetworkVariable<int> solvedStatus = new NetworkVariable<int>(0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Solve()
    {
        isSolved.Value = true;
    }

    public void NextCode()
    {
        solvedStatus.Value++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
