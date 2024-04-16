using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hintable : MonoBehaviour
{
    // all Hintable's have an associated Hint that is triggered given conditions defined by the Hintable
    private Hint hint;
    public void Start () {
        hint = GetComponent<Hint>();
    }
    public virtual void TriggerHint() {
        if (hint != null) hint.DisplayHint();   // at the very least a Hintable will display the respective Hint
    }
}
