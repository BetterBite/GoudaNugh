using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastBook : PastObject
{
    private int vars;
    public Rigidbody leftBody;
    public Rigidbody rightBody;
    private bool isLocked = true;
    public override void Setup()
    {
        
    }

    public void Unlock()
    {
        leftBody.isKinematic = true;
        rightBody.isKinematic = true;
        leftBody.constraints = RigidbodyConstraints.None;
        rightBody.constraints = RigidbodyConstraints.None;
        leftBody.isKinematic = false;
        rightBody.isKinematic = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
