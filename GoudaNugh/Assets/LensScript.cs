using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensScript : MonoBehaviour
{
    public GameObject lever;
    public bool rotatable;
    public Transform symbol;
    public Transform marker;
    private ContinuousRotatableCheck leverRotator;
    public FutureTelescope telescope;
    // Start is called before the first frame update
    void Awake()
    {
        leverRotator = lever.GetComponent<ContinuousRotatableCheck>();
    }

    public void CheckLens(int lens)
    {
        
        float diff = transform.rotation.eulerAngles.z - telescope.angles[lens];
        if (diff < 10 && diff > -10)
        {
            Debug.Log("Lens Pass");

            gameObject.GetComponent<Renderer>().material.color = Color.blue;
            lever.SetActive(false);

            telescope.NextCode();
            return;
        }
        else return;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.rotation.eulerAngles);
        if (leverRotator.activated)
        {
            gameObject.transform.Rotate(new Vector3(0, 0, 0.5f));
            
            //Debug.Log(Vector3.Distance(symbol.position, marker.position));
        }

        //Debug.Log(lever.activated);
        //if (lever.activated)
        //{
        //    Debug.Log("turning");
        //    gameObject.transform.Rotate(new Vector3(0, 0, 1));
        //}
    }
}
