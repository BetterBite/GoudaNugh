using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// is this class even necessary? Oh this class collides with Hintable triggers. This assumes that the Hintables have a trigger collider attached (i think). Can't remeber which way round it is
// do we attach this to the hands?
public class HintSystemManager : MonoBehaviour
{
    public float SecondsBuffer = 2.0f;
    private Dictionary<Hintable, float> hintableDict = new Dictionary<Hintable, float>();
    public void OnTriggerEnter(Collider collider) {
        Hintable hintable = collider.gameObject.GetComponent<Hintable>();
        if (hintable == null) return;

        // add hintable to dictionary for checking if hintable is in range for long enough
        hintableDict.Add(hintable, System.DateTime.Now.Millisecond);
    }

    public void OnTriggerStay(Collider collider) {
        Hintable hintable = collider.gameObject.GetComponent<Hintable>();
        if (hintable == null) return;

        hintableDict.TryGetValue(hintable, out float start_time);
        if (System.DateTime.Now.Millisecond > start_time + SecondsBuffer*1000) {
            hintable.TriggerHint();         // if collider has triggered for longer than interval then trigger hint
            hintableDict.Remove(hintable);
        }
    }

    public void OnTriggerExit(Collider collider) {
        Hintable hintable = collider.gameObject.GetComponent<Hintable>();
        if (hintable != null) hintableDict.Remove(hintable);
    }
}
