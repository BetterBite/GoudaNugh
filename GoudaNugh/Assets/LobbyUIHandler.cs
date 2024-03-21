using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyUIHandler : MonoBehaviour
{
    [System.Serializable]
    public struct Pages {
        public GameObject page1;
        public GameObject page2Host;
        public GameObject page2Client;
        public GameObject page3Host;
        public GameObject page3Client;
    }
    public Pages pages;
    public bool isHost = false;
    private bool unknownHost = true;
    public string joinCodeForDisplay;
    private GameObject activePage;
    public TextMeshProUGUI myTextMeshPro;

    void Start() {
        activePage = pages.page1;

    }

    public void SetIsHost(bool host) {
        isHost = host;
        unknownHost = false;
    }

    public void SetJoinCodeForDisplay(string joinCode)
    {
        joinCodeForDisplay = joinCode;
    }

    public void SetState(int newState) {

        // hide current UI page
        activePage.SetActive(false);

        if (newState == 0) {
            // show create/join buttons
            activePage = pages.page1;

        } else if (newState == 1) {
            if (unknownHost) {} // throw error

            if (isHost) {
                // show creating server
                activePage = pages.page2Host;
            } else {
                // show connecting 
                activePage = pages.page2Client;
            }
        } else if (newState == 2) {
            if (unknownHost) {} // throw error

            if (isHost) {
                // show start game button
                activePage = pages.page3Host;
                // myTextMeshPro = GetComponent<TextMeshProUGUI>();
                myTextMeshPro.text = "JOIN CODE: " + joinCodeForDisplay + "\n\nSTART GAME";
            } else {
                // show waiting for host to start game
                activePage = pages.page3Client;
            }
        }

        activePage.SetActive(true);
    }
}
