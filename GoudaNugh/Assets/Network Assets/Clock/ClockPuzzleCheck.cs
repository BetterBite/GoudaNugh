using System.Security.Cryptography;
using UnityEngine;

public class ClockPuzzleCheck : MonoBehaviour
{

    public PastClock clock;
    // ===== PUBLIC PARAMETERS =====
    public Transform hour_transform;
    public Transform minute_transform;

    public int hour_target;
    public int minute_target;
    public int leniency = 10; // The degrees that the user can be 'off' by and still get the visual and audio feedback

    public AudioSource correct_feedback;    // The AudioSource to be played on completion

    // ===== END PUBLIC PARAMETERS =====

    private Quaternion target_hour_rotation;
    private Quaternion target_minute_rotation;

    private bool already_within_limit = false;  // this locks the conditional in Update so the 'true' segment is only ran once until the hands are removed and replaced into the correct limits.


    // Update is called once per frame
    void Update()
    {

        target_hour_rotation = TargetToQuaternion(hour_target);
        target_minute_rotation = TargetToQuaternion(minute_target);

        if (WithinLimit(hour_transform.rotation, target_hour_rotation, leniency) &&
            WithinLimit(minute_transform.rotation, target_minute_rotation, leniency) &&
            !already_within_limit)
        {
            // Hands in correct place!
            already_within_limit = true;

            Debug.Log("Correct config");
            correct_feedback.Play();
            clock.Solve();
        } else {
            // Hands not in correct place

            already_within_limit = false;
        }
    }

    // HELPERS

    // Returns True if the hand_rotation is close enough (defined by leniency parameter) to the target rotation.
    bool WithinLimit(Quaternion hand_rotation, Quaternion target, int leniency) {

        return Quaternion.Angle(hand_rotation, target) - leniency < 0;
    }

    // The hand being in the 12 position (straight upwards) has a quaternion of (-90, -90, 90). Therefore we offset the x component by -90 degrees.
    Quaternion TargetToQuaternion(int degrees) {
        return Quaternion.Euler(degrees - 90, -90, 90);
    }
}
