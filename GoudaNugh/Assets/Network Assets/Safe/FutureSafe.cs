using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Assertions;

public class FutureSafe : FutureObject {

    public LockableObject lockableObject;
    // FutureSafe does NOT need a reference to SafeCode as it does not exist in the future

    public override void Setup() {
        Assert.IsNotNull(lockableObject, "Lockable object script has not been set for this instance of safe!");
        networkObject.GetComponent<SafeVariables>().isLocked.OnValueChanged += OnIsLockedChanged;
    }

    private void OnIsLockedChanged(bool previousValue, bool newValue) {
        if (newValue) {
            lockableObject.Lock();
        } else {
            lockableObject.Unlock();
        }
    }
}
