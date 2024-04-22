using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;
using UnityEngine.Assertions;

public class SafeCode : MonoBehaviour {
    public int[] code = { 0, 0, 0};
    public TMP_Text[] content;
    // Important! Lockable object only gets set if the safe is spawned by the manager, otherwise you will get a NullReferenceException!
    public LockableObject lockableObject;
    //public Hinge hinge;

    void Start() {
        InteractibleManager IManager = InteractibleManager.Singleton;
        Assert.IsNotNull(IManager, "Interactible Manager not found! Make sure it has been added to the scene!");
        code[0] = IManager.SafeCode[0];
        code[1] = IManager.SafeCode[1];
        code[2] = IManager.SafeCode[2];
        Debug.Log("Code generated: " + code[0] + code[1] + code[2]);
    }

    // Returns false if the code is correct
    // Note: This function updates isLocked, hence, false is the value that is returned when the code is correct
    public bool CheckCode() {
        int count = 0;
        for (int i = 0; i < 3; i++) {
            if (code[i] == Int32.Parse(content[i].text)) {
                count++;
            } 
        }
        if (count>2) {
            Debug.Log("correct code");
            lockableObject.Unlock();
            // This object is two deep in the hierarchy so this ugly mess is required to get the grandparent
            gameObject.transform.parent.gameObject.transform.parent.GetComponent<PastSafe>().UnlockNetworkSafe();
            //hinge.open();
            return false;
        }
        return true;
    }
}
