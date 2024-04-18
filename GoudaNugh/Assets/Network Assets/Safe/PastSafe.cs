using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PastSafe : PastObject {

    public LockableObject lockableObject;
    public SafeCode safeCode;

    public override void Setup() {
        Assert.IsNotNull(lockableObject, "Lockable object script has not been set for this instance of safe!");
        Assert.IsNotNull(safeCode, "Safe code script has not been set for this instance of safe!");

        // Check code incase it spawns unlocked
        networkObject.GetComponent<SafeVariables>().isLocked.Value = safeCode.CheckCode();
        lockableObject.isLocked = networkObject.GetComponent<SafeVariables>().isLocked.Value;
        // TODO - Possibly add some custom logic for the safe if it starts open (e.g. make it look sligtly open)
    }

    public void Update() {
        if (!safeCode.CheckCode()) { 
            networkObject.GetComponent<SafeVariables>().isLocked.Value = false;
            lockableObject.Unlock();
        }
    }
}
