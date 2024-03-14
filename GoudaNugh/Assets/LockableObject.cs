using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockableObject : MonoBehaviour
{
    private Rigidbody body;
    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private bool isLocked;
    // Start is called before the first frame update
    void Start()
    {
        isLocked = false;
        startingPosition = transform.position;
        startingRotation = transform.rotation;
        body = GetComponent<Rigidbody>();
        
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
        if (!isLocked)
        {
            transform.position = startingPosition;
            transform.rotation = startingRotation;
            body.constraints = RigidbodyConstraints.FreezeAll;
            isLocked = true;
        }
    }

    public void Unlock()
    {
        if (isLocked)
        {
            body.constraints = RigidbodyConstraints.None;
            isLocked = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
