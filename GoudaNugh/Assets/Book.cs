using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    private int vars;
    public Rigidbody leftBody;
    public Rigidbody rightBody;
    private bool isLocked = true;

    public void Unlock()
    {
        leftBody.isKinematic = true;
        rightBody.isKinematic = true;
        leftBody.constraints = RigidbodyConstraints.None;
        rightBody.constraints = RigidbodyConstraints.None;
        leftBody.isKinematic = false;
        rightBody.isKinematic = false;
    }
}
