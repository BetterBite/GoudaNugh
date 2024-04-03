using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LockableObject : MonoBehaviour
{
    private Rigidbody body;
    private Vector3 startingPosition;
    private Quaternion startingRotation;
    public bool isLocked;
    // Start is called before the first frame update
    void Awake()
    {
        //isLocked = false;
        startingPosition = transform.position;
        startingRotation = transform.rotation;
        body = GetComponent<Rigidbody>();
        if (isLocked)
            Lock();
        else
            Unlock();
        
    }

    public void ToggleLock()
    {
        if (isLocked)
        {
            body.constraints = RigidbodyConstraints.None;
            isLocked = false;
        }
        else
        {
            transform.position = startingPosition;
            transform.rotation = startingRotation;
            body.constraints = RigidbodyConstraints.FreezeAll;
            isLocked = true;
        }
    }

    public void Lock()
    {

        transform.position = startingPosition;
        transform.rotation = startingRotation;
        body.constraints = RigidbodyConstraints.FreezeAll;
        isLocked = true;
    }

    public void Unlock()
    {

        body.constraints = RigidbodyConstraints.None;
        isLocked = false;
        body.AddForce(Vector3.forward*600000);

    }


    // Update is called once per frame
    void Update()
    {
    }
}
