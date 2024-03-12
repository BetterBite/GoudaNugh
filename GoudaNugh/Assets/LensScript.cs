using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensScript : MonoBehaviour
{
    public GameObject lever;
    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (lever.GetComponent<ContinuousRotatableCheck>().activated)
        {
            gameObject.transform.Rotate(new Vector3(0, 0, 0.5f));
        }
        //Debug.Log(lever.activated);
        //if (lever.activated)
        //{
        //    Debug.Log("turning");
        //    gameObject.transform.Rotate(new Vector3(0, 0, 1));
        //}
    }
}
