using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CabinetVariables : Variables
{
    // Start is called before the first frame update
    public NetworkVariable<bool> isLocked = new NetworkVariable<bool>(true);

    public void Unlock()
    {
        isLocked.Value = false;
    }
}
