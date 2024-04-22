using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LockableObject : MonoBehaviour {
    private Rigidbody body;
    private Vector3 startingPosition;
    private Quaternion startingRotation;
    public bool isLocked;
    public Vector3 vectorToPush;

    void Awake() {
        //isLocked = false;
        startingPosition = transform.position;
        startingRotation = transform.rotation;
        body = GetComponent<Rigidbody>();
        if (isLocked) Lock();
        else Unlock();
    }

    public void ToggleLock() {
        if (isLocked) {
            body.constraints = RigidbodyConstraints.None;
            isLocked = false;
        }
        else {
            transform.SetPositionAndRotation(startingPosition, startingRotation);
            body.constraints = RigidbodyConstraints.FreezeAll;
            isLocked = true;
        }
    }

    public void Lock() { 
        transform.SetPositionAndRotation(startingPosition, startingRotation);
        body.constraints = RigidbodyConstraints.FreezeAll;
        isLocked = true;
    }

    public void Unlock(){
        body.constraints = RigidbodyConstraints.None;
        isLocked = false;
        body.AddForce(vectorToPush);
    }
}
