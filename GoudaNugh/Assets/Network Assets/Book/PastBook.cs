using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastBook : PastObject {
    [SerializeField]
    private BookVariables vars;
    
    public override void Setup() {
        vars = networkObject.GetComponent<BookVariables>();
    }

    public void Solve() {
        vars.Solve();
    } 
}
