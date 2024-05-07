using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureTM : FutureObject
{
    private TMVariables vars;
    public GameObject disc;
    public GameObject confession;
    public override void Setup()
    {
        vars = networkObject.GetComponent<TMVariables>();
        vars.hasDisc.OnValueChanged += AddDisc;
        vars.isSolved.OnValueChanged += Solve;
    }

    private void AddDisc(bool hadDisc, bool hasDisc) 
    {
        disc.SetActive(true);
    
    }

    private void Solve(bool wasSolved, bool solved)
    {
        StartCoroutine(Confess());
    }

    public void Charge()
    {
        vars.ChargeRpc();
    }

    // Start is called before the first frame update


    private IEnumerator Confess()
    {
        yield return new WaitForSeconds(5f);
        confession.SetActive(true);

    }

}
