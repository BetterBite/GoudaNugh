using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BookVariables : Variables
{

    public NetworkVariable<bool> isLocked = new NetworkVariable<bool>(true);
    
    // Start is called before the first frame update
    public void Solve()
    {
        isLocked.Value = false;
    }
}
