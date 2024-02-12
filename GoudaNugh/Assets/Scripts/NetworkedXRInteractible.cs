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
        _grabInteractible.selectEntered.AddListener(LogSelect);
    }

    private void LogSelect(SelectEnterEventArgs args)
    {
        Debug.Log("Selected");
        NetworkObject interactorNetworkObject;
        MonoBehaviour interactor = args.interactorObject as MonoBehaviour;
        if (interactor != null) {
            Debug.Log("Interactor found");
            interactorNetworkObject = interactor.GetComponent<NetworkObject>();
            Debug.Log(interactorNetworkObject);
            if (interactorNetworkObject != null) {
                if (_networkObject.IsOwner) {
                    Debug.Log("Owner selected");
                }
                else {
                    Debug.Log("Non-owner selected");
                }
            }
            else {
                Debug.LogError(string.Format("Could not find NetworkObject for the interactor! Interactor is: {0} and found NetworkObject: {1}", args.interactorObject.ToString(), interactorNetworkObject));
            }
        }
    }
}
