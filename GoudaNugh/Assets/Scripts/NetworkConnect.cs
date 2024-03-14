using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode.Transports.UTP;
using UnityEngine.SceneManagement;

public class NetworkConnect : MonoBehaviour
{
    public int maxConnection = 2;
    public UnityTransport transport;
    public string joinCode;

    public Transform p1Pos;
    public Transform p2Pos;
    public Transform rig;
    public LobbyUIHandler uiHandler;

    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

    }


    public async void Create()
    {

        // File -> Build Settings -> Scenes in Build -> assign to the function below a numer of a scene to get player to

        // we don't want to load scene on create.
        // SceneManager.LoadScene("BetaSceneMain");
        uiHandler.SetIsHost(true);
        uiHandler.SetState(1);


        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnection);
        string newJoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

        Debug.LogError(newJoinCode);

        transport.SetHostRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
             allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData);

        NetworkManager.Singleton.StartHost();

        // display Start Game button
        uiHandler.SetState(2);

        // We don't want to move the player on create.
        // rig.position = p1Pos.position;

    }

    public async void Join()
    {

        // File -> Build Settings -> Scenes in Build -> assign to the function below a numer of a scene to get player to
        // We don't want to load scene on join
        // SceneManager.LoadScene("BetaSceneMain");
        uiHandler.SetIsHost(false);
        uiHandler.SetState(1);

        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

        transport.SetClientRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
           allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData, allocation.HostConnectionData);

        NetworkManager.Singleton.StartClient();

        // show "Waiting for Host to Start game"
        uiHandler.SetState(2);

        // We don't want to move player on join
        // rig.position = p2Pos.position;

        
    }

    public async void Start() 
    {
        // TODO: Implement starting the game
        // This method is called by the Host when clicking the "Start Game" button in the lobby.

        // SceneManager.LoadScene("BetaSceneMain");
    }
}

