using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.VRTemplate;

public class PastDial : NetworkBehaviour
{
    
    public NetworkVariable<bool> isLocked = new NetworkVariable<bool>(true);
    public Transform transform;
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {

    }

    void checkRotation()
    {
        //Debug.Log(transform.localRotation.eulerAngles.y);
        if (transform.localRotation.eulerAngles.y == 0)
        {
            Debug.Log("unlocked");

            this.gameObject.GetComponent<XRKnob>().enabled = false;
            UnlockServerRpc();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(IsServer)
        {
            checkRotation();
        }
        if(IsClient)
        {
        }
            
    }


    [ClientRpc]
    private void DisableDoorClientRpc()
    {
        if (IsClient)
        {
            door.SetActive(false);
            Debug.LogError("Door disabled");
        }

    }


    [ServerRpc]
    private void UnlockServerRpc()
    {
        isLocked.Value = false;
        DisableDoorClientRpc();
        
    }
}
