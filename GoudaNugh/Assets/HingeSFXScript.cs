using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeSFXScript : MonoBehaviour
{
    public float x, y, z;
    public AudioSource openSound;
    public AudioSource closedSound;
    private bool opened;
    // Start is called before the first frame update
    void Start()
    {
        //x = gameObject.transform.position.x;
        opened = false;
    }
    void OnTriggerEnter(Collider collision){
        if (opened == false) {
            openSound.Play();
            Debug.Log("Opened!");
            opened = true;
        }
        else {
            closedSound.Play();
            Debug.Log("Closed!");
            opened = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
   //     if (opened == false){
    //    if (gameObject.transform.position.x != x){
     //       openSound.Play();
     //       Debug.Log("Opened!");
    //        opened = true;
    //    }
    //    }
    //    else{
    //        if (gameObject.transform.position.x == x){
    //        closedSound.Play();
    //       opened = false;
    //    }
    //    }
    }
}
