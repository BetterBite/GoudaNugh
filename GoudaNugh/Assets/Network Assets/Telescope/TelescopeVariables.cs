using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TelescopeVariables : Variables
{

    public NetworkVariable<bool> isSolved = new NetworkVariable<bool>(false);
    public NetworkVariable<int> solvedStatus = new NetworkVariable<int>(0);

    public void Solve()
    {
        isSolved.Value = true;
    }

    public void NextCode()
    {
        solvedStatus.Value++;
    }
}
