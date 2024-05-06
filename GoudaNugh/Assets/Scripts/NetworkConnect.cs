using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Vivox;
using Unity.Services.Relay.Models;
using Unity.Netcode.Transports.UTP;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class NetworkConnect : MonoBehaviour {
    public int maxConnection = 2;
    public UnityTransport transport;
    public string joinCode;
    public Transform p1Pos;
    public Transform p2Pos;
    public Transform rig;
    public LobbyUIHandler uiHandler;
    public TMP_InputField myTextMeshProInputJoinCode;
    [SerializeField]
    private Toggle VoiceToggle;

    private async void Awake() {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        VoiceToggle.onValueChanged.AddListener(delegate{VivoxToggle(VoiceToggle);});
    }

    void VivoxToggle(Toggle voiceToggle) {
        Debug.Log("Voice " + voiceToggle.isOn);
    }

    public async void SetJoinCode() {
        joinCode = myTextMeshProInputJoinCode.text;
        //joinCode = joinCode.Trim();
    }

    public async void Create() {
        uiHandler.SetIsHost(true);
        uiHandler.SetState(1);

        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnection);
        string newJoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

        Debug.Log("JOIN CODE: " + newJoinCode);

        transport.SetHostRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
             allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData);

        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.OnSceneEvent += Spawner.Singleton.CheckSceneEvent;
        // display Start Game button
        uiHandler.SetJoinCodeForDisplay(newJoinCode);
        uiHandler.SetState(2);
    }


    public async void Join() {
        uiHandler.SetIsHost(false);
        uiHandler.SetState(1);
        Debug.Log("Join code being used: " + joinCode);
        // Ensure joinCode is not null or empty
        if (string.IsNullOrWhiteSpace(joinCode)) {
            Debug.LogError("Join code is empty or null. Please set a valid join code.");
            return; // Exit the method to avoid making a request with invalid join code
        }
        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
        transport.SetClientRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
           allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData, allocation.HostConnectionData);
        NetworkManager.Singleton.StartClient();

        // show "Waiting for Host to Start game"
        uiHandler.SetState(2);
    }

    public void StartGame() {
        Debug.Log("Starting Game");
        NetworkManager.Singleton.SceneManager.LoadScene("BetaSceneSchoolsDayFixes", LoadSceneMode.Single);
        // InteractibleManager.Singleton.OnSceneLoad();
        // Why is this here if its being done by the network manager??? We are double loading the scene
    }
}