using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotatableCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public int threshold;
    public float[] triggerAngles;
    public UnityEvent[] actions;
    private HingeJoint joint;
    private int axis;

    void Start()
    {
        threshold = 5;
        joint = GetComponent<HingeJoint>();
        for (int i = 0; i < 3; i++)
        {
            if (joint.axis[i] == 1)
            {
                axis = i;
            }
        }

    }

    public void CheckPosAfterRelease()
    {
        float rotation = transform.rotation.eulerAngles[axis];

        for (int i = 0; i < triggerAngles.Length; i++)
        {
            float diff = (rotation - triggerAngles[i]);

            if (diff < threshold && diff > -threshold)
            {
                Debug.Log("Correct position!");
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
