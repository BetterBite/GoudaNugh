using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TMVariables : Variables
{
    public NetworkVariable<bool> isSolved = new NetworkVariable<bool>(false);
    public NetworkVariable<bool> isCharged = new NetworkVariable<bool>(false);
    public NetworkVariable<bool> hasDisc = new NetworkVariable<bool>(false);


    public void AddDisc()
    {
        hasDisc.Value = true;
        if (isCharged.Value) 
        {
            Solve();
        }
    }
    [Rpc(SendTo.ClientsAndHost)]
    public void ChargeRpc()
    {
        isCharged.Value = true;
        if (hasDisc.Value)
        {
            Solve();
        }
        
    }
    public void Solve()
    {
        isSolved.Value = true;
    }


}
