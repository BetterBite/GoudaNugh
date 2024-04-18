using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Assertions;

public class FutureSafe : FutureObject {

    public LockableObject lockableObject;
    public SafeCode safeCode;

    public override void Setup() {
        Assert.IsNotNull(lockableObject, "Lockable object script has not been set for this instance of safe!");
        Assert.IsNotNull(safeCode, "Safe code script has not been set for this instance of safe!");
    }
}
