using System.Collections;
using System.Collections.Generic;
using Oculus.VoiceSDK.UX;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class RadioLever : MonoBehaviour
{

    private HingeJoint joint;
    private int axis;
    public Sinewave wave;

    private float topAngle = 30;
    private float bottomAngle = 150;
    private float prevAngle;

    public bool isPast;
    
    
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
        prevAngle = transform.rotation.eulerAngles[axis];
    }



    // Update is called once per frame
    void Update()
    {
        float interpolatedAngle = 0;
        float angle = transform.rotation.eulerAngles[axis];
        if ( angle != prevAngle)
        {
            interpolatedAngle = (bottomAngle - angle) / (bottomAngle - topAngle);
            
            if (isPast)
            {
                wave.amplitude = Mathf.Lerp(0, 0.1f, interpolatedAngle);
            }
            if(!isPast)
            {
                UpdateFrequencyServerRpc(interpolatedAngle);
                wave.frequency = Mathf.Lerp(0, 20, interpolatedAngle);
            }
            
            prevAngle = angle;
        }
        //Debug.Log("Angle: " + angle);
        Debug.Log("interpolated value: " + interpolatedAngle);
    }

    [Rpc(SendTo.Server)]
    public void UpdateFrequencyServerRpc(float interpolatedAngle)
    {
        Debug.Log("LoL");
        wave.frequency = Mathf.Lerp(0, 20, interpolatedAngle);
    }
}


