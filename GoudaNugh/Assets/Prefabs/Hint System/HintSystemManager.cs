using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// is this class even necessary? Oh this class collides with Hintable triggers. This assumes that the Hintables have a trigger collider attached (i think). Can't remeber which way round it is
// do we attach this to the hands?
public class HintSystemManager : MonoBehaviour
{
    public float SecondsBuffer = 2.0f;
    private Dictionary<Hintable, long> hintableDict = new Dictionary<Hintable, long>();
    private const int TICKS_PER_SECOND = 10000000;
    public void OnTriggerEnter(Collider collider) {
        Hintable hintable = collider.gameObject.GetComponent<Hintable>();
        if (hintable == null) return;

        // add hintable to dictionary for checking if hintable is in range for long enough
        hintableDict.Add(hintable, System.DateTime.Now.Ticks);
    }

    public void OnTriggerStay(Collider collider) {
        Hintable hintable = collider.gameObject.GetComponent<Hintable>();
        if (hintable == null) return;

        if (!hintableDict.TryGetValue(hintable, out long start_time)) return;
        if (System.DateTime.Now.Ticks - start_time > SecondsBuffer*TICKS_PER_SECOND) {
            hintable.TriggerHint();         // if collider has triggered for longer than interval then trigger hint
            hintableDict.Remove(hintable);
        }
    }

    public void OnTriggerExit(Collider collider) {
        Hintable hintable = collider.gameObject.GetComponent<Hintable>();
        if (hintable != null) hintableDict.Remove(hintable);
    }
}