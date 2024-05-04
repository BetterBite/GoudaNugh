using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PastGlobeLever : MonoBehaviour
{
    // Start is called before the first frame update
    //public int threshold;
    public float triggerAngle;
    //public UnityEvent[] actions;
    private HingeJoint joint;
    private int axis;
    private float yAxisStart;
    public bool grabbed;
    public bool activated;
    private Quaternion startRot;

    public float velocity;
    public CounterMove counter;
    public PastGlobe globe;

    void Awake()
    {

        startRot = transform.rotation;
        yAxisStart = startRot[1];
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
        globe.OnGrab(true);
    }

    public void OnUngrab()
    {
        grabbed = false;
        activated = false;
        globe.OnGrab(false);
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
            velocity = angle * 10;
        }
        Vector3 vec = new Vector3(0, velocity, 0);
        //counter.rotate(vec);
        globe.PastMove(vec);


    }

    void Update()
    {
        if (grabbed)
        {
            float angle = transform.rotation[1] - yAxisStart;
            Spin(angle);

            //Debug.Log(angle);

            //activated = (transform.rotation.eulerAngles[axis] < triggerAngle);
            //Debug.Log(activated);

        }
        //Debug.Log(activated);
    }
}
