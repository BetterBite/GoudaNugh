using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class JournalScript : MonoBehaviour
{
    public GameObject Evidence;
    public GameObject NotedEv;
    //XRGeneralGrabTransformer GenGrabTrans;
    // Start is called before the first frame update
    public void EvidenceFound()
    {
        NotedEv.SetActive(true);
    }
    void Start()
    {
        NotedEv.SetActive(false);
        //GenGrabTrans= Evidence.GetComponent<XRGeneralGrabTransformer>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (GenGrabTrans.isFound == true){
       //     NotedEv.SetActive(true);
        //}
    }
}


