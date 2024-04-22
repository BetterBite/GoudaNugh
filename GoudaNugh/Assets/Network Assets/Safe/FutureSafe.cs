using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Assertions;

public class FutureSafe : FutureObject {

    public LockableObject lockableObject;
    [SerializeField]
    private GameObject itemInsidePrefab;
    // FutureSafe does NOT need a reference to SafeCode as it does not exist in the future

    public override void Setup() {
        Assert.IsNotNull(lockableObject, "Lockable object script has not been set for this instance of safe!");
        Assert.IsNotNull(itemInsidePrefab, "Item inside prefab has not been set for this instance of safe!");
        networkObject.GetComponent<SafeVariables>().isLocked.OnValueChanged += OnIsLockedChanged;
    }

    private void OnIsLockedChanged(bool previousValue, bool newValue) {
        if (newValue) {
            lockableObject.Lock();
            // Unesscessary relocking of locked safe. Possibly remove
        } else {
            lockableObject.Unlock();
            itemInsidePrefab.SetActive(true);
            // This object should be instantiated inside but for now it is pre-existing in the prefab and disabled
        }
    }
}
