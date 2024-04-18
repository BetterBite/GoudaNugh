using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SafeVariables : Variables {

    public NetworkVariable<bool> _isLocked = new NetworkVariable<bool>(true);

    public NetworkVariable<bool> isLocked { get => _isLocked; set => _isLocked = value; }
}
