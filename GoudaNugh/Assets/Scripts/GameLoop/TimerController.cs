using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
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

public class TimerController : MonoBehaviour
{
    [SerializeField] Image timerGraphic;
    [SerializeField] float gameTimeInSeconds;

    float maxGameTime;

    private void Awake()
    {
        maxGameTime = gameTimeInSeconds;

    }

    // Update is called once per frame
    void Update()
    {
        gameTimeInSeconds -= Time.deltaTime;

        var updateTimerGraphicValue = gameTimeInSeconds / maxGameTime;

        timerGraphic.fillAmount = updateTimerGraphicValue;

        if (gameTimeInSeconds<=0)
        {
            Debug.Log("Loading Lobby");
            NetworkManager.Singleton.SceneManager.LoadScene("LobbyForGameFinished", LoadSceneMode.Single);
        }
    }
}
