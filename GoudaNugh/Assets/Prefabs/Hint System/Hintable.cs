using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Hintable : MonoBehaviour
{
    // all Hintable's have an associated Hint that is triggered given conditions defined by the Hintable
    private Hint hint;

    [Header("Hintable Configuration")]
    public bool ProximityTriggeringEnabled = true;
    public bool EventTriggeringEnabled = true;
    public void Start () {
        hint = GetComponent<Hint>();
    }
    public virtual void TriggerHint() {
        if (hint != null) hint.DisplayHint();   // at the very least a Hintable will display the respective Hint
    }
    public virtual void TriggerHintFromHintManager() {
        if (ProximityTriggeringEnabled) TriggerHint();
    }
    public virtual void TriggerHintFromEvent(Action e) { // subscribe to a given event and trigger the hint based off of that event.
        if (EventTriggeringEnabled) e += TriggerHint;
    }
}
