using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttachableCheck : MonoBehaviour
{
    [SerializeField] private UnityEvent onAttach; 
    public ClueLinker.Links iwantthis;
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
        Attachable attachable = other.gameObject.GetComponent<Attachable>();
        if (attachable != null && attachable.link == iwantthis) { 
        
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
        if (solid != null) solid.SetActive(true);
        GetComponent<Collider>().enabled = false;
        if (intersecting != null)
        {
            intersecting.SetActive(false);
        }

        Debug.Log("Attachable detected");
        onAttach.Invoke();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
