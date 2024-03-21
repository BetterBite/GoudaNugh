using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensScript : MonoBehaviour
{
    public GameObject lever;
    private ContinuousRotatableCheck leverCheck;
    private bool solved = false;
    // Start is called before the first frame update
    void Awake()
    {
        leverCheck = lever.GetComponent<ContinuousRotatableCheck>();
    }

    public void SolveLens()
    {
        solved = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (leverCheck.activated && !solved)
        {
            gameObject.transform.Rotate(new Vector3(0, 0, 0.5f));
        }
    }
}
