using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class FutureGlobeLever : MonoBehaviour
{
    // Start is called before the first frame update
    //public int threshold;
    public float triggerAngle;
    //public UnityEvent[] actions;
    private HingeJoint joint;
    private int axis;
    private float rotation;
    public bool grabbed;
    public bool activated;
    private Quaternion startRot;

    public float velocity;
    public CounterMove counter;
    public FutureGlobe globe;

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

    private void Spin(float angle)
    {

        if (angle > 0.1 && angle < -0.1) velocity = 0f;
        else
        {
            velocity = angle * 5;
        }
        Vector3 vec = new Vector3(0, 0, velocity);
        //counter.rotate(vec);
        globe.FutureMove(vec);


    }

    void Update()
    {
        if (grabbed)
        {
            float angle = transform.rotation[axis];
            //if (angle > 90) angle = -1 * angle + 180;
            Spin(angle);

            activated = (transform.rotation.eulerAngles[axis] < triggerAngle);
            //Debug.Log(activated);

        }
        //Debug.Log(activated);
    }
}
