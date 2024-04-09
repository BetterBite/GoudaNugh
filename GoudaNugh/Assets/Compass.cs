using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public Transform north;
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
        if(baseRot.x < 0.1 && baseRot.x > -0.1) 
        {
            if (baseRot.z < 0.1 && baseRot.z > -0.1)
            {
                if (!isFlat)
                {
                    needle.SetActive(true);
                    dummy.SetActive(false);
                }
                
                Vector3 direction = north.transform.position - transform.position;
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
                dummy.transform.rotation = needle.transform.rotation;
                dummy.transform.position = needle.transform.position;
                needle.SetActive(false);
                isFlat = false;
            }

            
        }

        //Debug.Log(Base.transform.rotation);
        //point to globe

    }
}
