using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Assertions;

public class ClockPuzzleCheck : MonoBehaviour {
    public Clock clock;
    public Transform hour_transform;
    public Transform minute_transform;

    public int hour_target_rotation;
    public int minute_target_rotation;
    public int leniency = 10; // The degrees that the user can be 'off' by and still get the visual and audio feedback

    public AudioSource correct_feedback;    // The AudioSource to be played on completion

    private bool already_within_limit = false;  // this locks the conditional in Update so the 'true' segment is only ran once until the hands are removed and replaced into the correct limits.

    [SerializeField]
    private bool ForceSoftCheck = false;

    private void Awake() {
        Assert.IsNotNull(clock, "Clock prefab reference not assigned for the Clock Puzzle script!");

    }

    public void OnValidate() {
        if (ForceSoftCheck) {
            Debug.Log("hour_target_rotation was identified as: " + hour_target_rotation);
            Debug.Log("Current rotation of hour hand before passing is: " + hour_transform.rotation.eulerAngles.x);
            Debug.Log("minute_target_rotation was identified as: " + minute_target_rotation);
            Debug.Log("Current rotation of minute hand before passing is: " + minute_transform.rotation.eulerAngles.x);
            ForceSoftCheck = false;
        }
    }

    void Update() { 
        if (hour_target_rotation-leniency < hour_transform.rotation.eulerAngles.x 
            && hour_transform.rotation.eulerAngles.x < hour_target_rotation+leniency 
            && minute_target_rotation-leniency < minute_transform.rotation.eulerAngles.x 
            && minute_transform.rotation.eulerAngles.x < minute_target_rotation+leniency
            && !already_within_limit) {
            Debug.Log("hour_target_rotation was identified upon passing the check as: " + hour_target_rotation);
            Debug.Log("Current rotation of hour hand upon passing is: " + hour_transform.rotation.eulerAngles.x);
            // Hands in correct place!
            already_within_limit = true;
            Debug.Log("Correct config");
            //correct_feedback.Play();
            clock.Solve();
        } else {
            return;
        }
    }
}
