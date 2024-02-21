using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.VRTemplate;

public class FutureDial : NetworkBehaviour
{

    public GameObject other;
    private bool otherIsLocked;
    public GameObject door;
    public GameObject figurine;
    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        var script = other.GetComponent<PastDial>();
        script.isLocked.OnValueChanged += OnValueChanged;

    }

    private void OnValueChanged(bool wasLocked, bool isLocked)
    {
        if (!isLocked)
        {
            door.SetActive(false);
            figurine.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(detectedGrab.Value);
    }
}
