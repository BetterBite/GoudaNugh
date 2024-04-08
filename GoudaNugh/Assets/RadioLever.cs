using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioLever : MonoBehaviour
{

    private HingeJoint joint;
    private int axis;
    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<HingeJoint>();
        for (int i = 0; i < 3; i++)
        {
            if (joint.axis[i] == 1)
            {
                axis = i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.rotation.eulerAngles[axis]);
    }
}
