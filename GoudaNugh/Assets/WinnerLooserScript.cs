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
public class WinnerLooserScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadWinnerScene()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("EndGameWinner", LoadSceneMode.Single);
    }

    // Update is called once per frame
    public void LoadLooserScene()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("EndGameLooser", LoadSceneMode.Single);
    }
}
