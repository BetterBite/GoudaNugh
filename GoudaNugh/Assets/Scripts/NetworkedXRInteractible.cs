using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkedXRInteractible : MonoBehaviour
{
    private XRGrabInteractable _grabInteractible;
    private NetworkObject _networkObject;
    private void Awake()
    {
        _grabInteractible = GetComponent<XRGrabInteractable>();
        _networkObject = GetComponent<NetworkObject>();
        _grabInteractible.selectEntered.AddListener(ChangeOwnerOnSelect);
        _grabInteractible.selectExited.AddListener(RelinquishObjectOnLetgo);
    }

    // Changes owner upon the object being grabbed.
    // IXRSelectInteractor does not have a method to obtain its NetworkObject component thus it needs to be cast to MonoBehaviour
    private void ChangeOwnerOnSelect(SelectEnterEventArgs args)
    {
        //Debug.Log("Selected");
        NetworkObject interactorNetworkObject;
        MonoBehaviour interactor = args.interactorObject as MonoBehaviour;
        if (interactor != null) {
            //Debug.Log("Interactor found");
            interactorNetworkObject = interactor.GetComponent<NetworkObject>();
            //Debug.Log(interactorNetworkObject);
            if (interactorNetworkObject != null) {
                if (_networkObject.IsOwner) {
                    Debug.Log("Owner selected");
                }
                else {
                    Debug.Log("Non-owner selected");
                    _networkObject.ChangeOwnership(interactorNetworkObject.OwnerClientId);
                }
            }
            else {
                Debug.LogError(string.Format("Could not find NetworkObject for the interactor! Interactor is: {0} and found NetworkObject: {1}", args.interactorObject.ToString(), interactorNetworkObject));
            }
        }
    }

    private void RelinquishObjectOnLetgo(SelectExitEventArgs args) 
    {
        if (_networkObject.IsOwner) {
            Debug.Log("Client relinquished object");
            _networkObject.RemoveOwnership();
        } else {
            Debug.Log("Server let go of the object");
        }
    }
}
