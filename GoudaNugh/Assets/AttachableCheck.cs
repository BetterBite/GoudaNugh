using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableCheck : MonoBehaviour
{
    public GameObject iwantthis;
    public GameObject ghost;
    public GameObject solid;
    private BoxCollider box;
    private GameObject intersecting;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == iwantthis)
        {
            ghost.SetActive(true);
            intersecting = other.gameObject;

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        ghost.SetActive(false);
        intersecting = null;
    }

    public void objectReleased()
    {
        Debug.Log(intersecting);
        if (intersecting != null)
        {
            Attach();
        }
    }

    private void Attach()
    {
        ghost.SetActive(false);
        solid.SetActive(true);
        GetComponent<Collider>().enabled = false;
        if (intersecting != null)
        {
            intersecting.SetActive(false);
        }

        Debug.Log("Attachable detected");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
