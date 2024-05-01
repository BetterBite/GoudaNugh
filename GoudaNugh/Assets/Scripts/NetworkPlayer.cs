using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using Unity.Services.Vivox;

public class NetworkPlayer : NetworkBehaviour
{

    public Transform root;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public Renderer[] meshToDisable;

    public Renderer[] ghostRenderers;

    public Material futureMaterial;
    public Material pastMaterial;

    public bool isPast;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            foreach (var item in meshToDisable)
            {
                item.enabled = false;
            }


            
        }


        if (IsHost)
        {
            isPast = true;
            Debug.Log("isPast: true");
            foreach (var item in ghostRenderers)
            {
                item.material = futureMaterial;
            }
        }
        else
        {
            isPast = false;
            Debug.Log("isPast: false");
            foreach (var item in ghostRenderers)
            {
                item.material = pastMaterial;
            }
        }

 

        ///
        var myID = transform.GetComponent<NetworkObject>().NetworkObjectId;
        if (IsOwnedByServer)
        {
            transform.name = "Host: " + myID;
        }
        else
        {
            transform.name = "Client: " + myID;
        }


        //Vivox
        var vTog = GameObject.Find("Toggle").GetComponent<Toggle>();
        //if (vTog.isOn)
        if (vTog.isOn && IsOwner)
        {
            Debug.Log("Toggle found");
            GameObject.Find("NetworkManager").GetComponent<VivoxPlayer>().SignIntoVivox();
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            root.position = VRRigReferences.Singleton.root.position;
            root.rotation = VRRigReferences.Singleton.root.rotation;

            head.position = VRRigReferences.Singleton.head.position;
            head.rotation = VRRigReferences.Singleton.head.rotation;

            leftHand.position = VRRigReferences.Singleton.leftHand.position;
            leftHand.rotation = VRRigReferences.Singleton.leftHand.rotation;

            rightHand.position = VRRigReferences.Singleton.rightHand.position;
            rightHand.rotation = VRRigReferences.Singleton.rightHand.rotation;
        }

    }
}
