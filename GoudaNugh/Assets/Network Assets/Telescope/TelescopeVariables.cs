using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TelescopeVariables : Variables
{
    public NetworkVariable<bool> isFixed = new NetworkVariable<bool>(false);
    public NetworkVariable<bool> isSolved = new NetworkVariable<bool>(false);
    public NetworkVariable<int> solvedStatus = new NetworkVariable<int>(0);

    public void Fix()
    {
        isFixed.Value = true;
    }

    public void Solve()
    {
        isSolved.Value = true;
    }

    [Rpc(SendTo.Server)]
    public void NextCodeServerRpc()
    {
        solvedStatus.Value++;
    }
}
