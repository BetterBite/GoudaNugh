using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class JournalScript : MonoBehaviour
{
    public GameObject[] Evidence;
    public GameObject NotedEv;
    private JournalEvidence journalEvidence;
    public int PageNum;
    public TMP_Text Txt;
    //XRGeneralGrabTransformer GenGrabTrans;
    // Start is called before the first frame update
    public void EvidenceFound()
    {
        NotedEv.SetActive(true);
    }
    public void ForwardPage()
    {

        if (PageNum < Evidence.Length);
        PageNum = PageNum + 1;
        LoadPage();

    }
    public void BackPage()
    {
        if (PageNum > 1){
        PageNum = PageNum - 1;
        LoadPage();
        }

    }
    void LoadPage()
    {
        for (int i = 0; i < Evidence.Length; i++){
        journalEvidence = Evidence[i].GetComponent<JournalEvidence>();
        if (journalEvidence.Page == PageNum){
            Evidence[i].SetActive(true);
        }
        else
        {
            Evidence[i].SetActive(false);
        }
        }
        Txt.text = PageNum.ToString();
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


