using Unity.VisualScripting;
using UnityEngine;

public class HandleMagnetise : MonoBehaviour
{
    private FixedJoint joint = null;
    private bool attached = false;

    public bool Magnetised = false;
    public int MagnetStrength = 1;
     public AudioSource correct_feedback;
    
    // magnets can stick to one other magnet with a higher magnet strength
    // initially connected to nothing. 
    // on collision should check if collider is a magnet (with the HandleMagnetise component).

    // if the magnet has a higher magnet strength,
    //     if the magnet has a higher magnet strength than the currently attached magnet, (or this is attached to nothing)
    //          create a fixed joint from this to that magnet

    private void OnCollisionEnter(Collision collision)
    {
        if (Magnetised) {
            if (collision.gameObject.TryGetComponent(out HandleMagnetise otherMagnet) && otherMagnet.Magnetised) {
                // two magnets have collided

                if (otherMagnet.MagnetStrength > MagnetStrength) {
                    // collided magnet attracts this magnet

                    // check if not already joined to a magnet
                    if (!attached)
                    {
                        Debug.Log(gameObject + " has no current attachment, attaching to "+ otherMagnet.gameObject);
                        joint = gameObject.AddComponent<FixedJoint>();
                        joint.enableCollision = true;

                        attach(otherMagnet.gameObject);

                    }
                        // check if this new magnet has higher magnetising power than any magnet currently connected.
                    else if (joint.connectedBody.gameObject.GetComponent<HandleMagnetise>().MagnetStrength < otherMagnet.MagnetStrength)
                    {
                        Debug.Log(gameObject + " is currently attached to "+joint.connectedBody.gameObject + ". Re-attaching to " + otherMagnet.gameObject);

                        // if it does, update the join so that the magnet is attracted to the newer more magnetising magnet.
                        attach(otherMagnet.gameObject);
                        correct_feedback.Play();
                    }
                }
            }
        }
    }

    void attach(GameObject obj) {
        joint.connectedBody = obj.GetComponent<Rigidbody>();
        attached = true;
    }
}
