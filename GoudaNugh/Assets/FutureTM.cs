using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureTM : FutureObject
{
    private TMVariables vars;
    public GameObject disc;
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

    }

    public void Charge()
    {
        vars.ChargeRpc();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
