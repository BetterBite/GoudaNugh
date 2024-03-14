using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class JournalScript : MonoBehaviour
{
    public GameObject Evidence;
    public GameObject NotedEv;
    public JournalEvidence journalEvidence;
    public int PageNum;
    //XRGeneralGrabTransformer GenGrabTrans;
    // Start is called before the first frame update
    public void EvidenceFound()
    {
        NotedEv.SetActive(true);
    }
    public void ForwardPage()
    {
        PageNum = PageNum + 1;
        LoadPage();

    }
    void LoadPage()
    {
        journalEvidence = Evidence.GetComponent<JournalEvidence>();
        if (journalEvidence.Page == PageNum){
            Evidence.SetActive(true);
        }
    }
    void Start()
    {
        PageNum = 1;
        LoadPage();
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


