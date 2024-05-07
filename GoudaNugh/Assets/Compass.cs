using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    float threshold = 0.05f;
    public Vector3 north;
    public GameObject needle;
    public Transform Base;
    public GameObject dummy;
    private bool isFlat;
    // Start is called before the first frame update
    void Start()
    {
        isFlat = false;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion baseRot = Base.transform.rotation;
        if(baseRot.x < threshold && baseRot.x > -threshold) 
        {
            if (baseRot.z < threshold && baseRot.z > -threshold)
            {
                if (!isFlat)
                {
                    needle.SetActive(true);
                    dummy.SetActive(false);
                }
                
                Vector3 direction = north - transform.position;
                direction.y = 0;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = targetRotation;
                isFlat = true;
            }
        } else
        {
            if (isFlat)
            {
                //dummy.SetActive(true);
               // dummy.transform.rotation = needle.transform.rotation;
                //dummy.transform.position = needle.transform.position;
                needle.SetActive(false);
                isFlat = false;
            }

            
        }

        //Debug.Log(Base.transform.rotation);
        //point to globe

    }
}
