using System.Collections;
using System.Collections.Generic;
using Oculus.VoiceSDK.UX;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PastRadioLever : MonoBehaviour
{
    public PastRadio radio;
    private HingeJoint joint;
    private int axis;
    public Sinewave wave;

    private float topAngle = 30;
    private float bottomAngle = 150;
    private float prevAngle;
    
    
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
            float lerp = Mathf.Lerp(0, 0.1f, interpolatedAngle);
            //wave.amplitude = lerp;
            radio.UpdateAmplitude(lerp);

            prevAngle = angle;
        }
    }

}


