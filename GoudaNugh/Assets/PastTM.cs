using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PastTM : PastObject
{
    private TMVariables vars;

    public GameObject gun;
    public override void Setup()
    {
        vars = networkObject.GetComponent<TMVariables>();
        vars.isCharged.OnValueChanged += Charge;
        vars.isSolved.OnValueChanged += Solve;
    }

    public void AddDisc()
    {
        vars.AddDisc();
    }

    private void Charge(bool wasCharged, bool Charged)
    {
        gun.SetActive(true);
    }

    private void Solve(bool wasSolved, bool solved)
    {
        
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
