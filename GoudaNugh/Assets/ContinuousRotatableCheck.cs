using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ContinuousRotatableCheck : MonoBehaviour
{
    // Start is called before the first frame update
    //public int threshold;
    public float triggerAngle;
    //public UnityEvent[] actions;
    private HingeJoint joint;
    private int axis;
    private float rotation;
    private bool grabbed;
    public bool activated;
    private Quaternion startRot;
    void Awake()
    {
        startRot = transform.localRotation;
        activated = false;
        //threshold = 5;
        joint = GetComponent<HingeJoint>();
        for (int i = 0; i < 3; i++)
        {
            if (joint.axis[i] == 1)
            {
                axis = i;
            }
        }
    }

    public void OnGrab()
    {
        grabbed = true;
    }

    public void OnUngrab()
    {
        grabbed = false;
        activated = false;
        transform.rotation = startRot;
    }

    //public void CheckPosAfterRelease()
    //{
    //    float rotation = transform.rotation.eulerAngles[axis];

    //    for (int i = 0; i < triggerAngles.Length; i++)
    //    {
    //        float diff = (rotation - triggerAngles[i]);

    //        if (diff < threshold && diff > -threshold)
    //        {
    //            Debug.Log("Correct position!");
    //        }
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        if (grabbed)
        {
            //Debug.Log(transform.rotation.eulerAngles[axis]);

            activated = (transform.rotation.eulerAngles[axis] < triggerAngle);
            //Debug.Log(activated);

        }
        //Debug.Log(activated);
    }
}
