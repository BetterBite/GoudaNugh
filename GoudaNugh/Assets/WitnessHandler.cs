using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.VoiceSDK.Utilities;
using Meta.Voice;
using Meta.WitAi;
using Oculus.Voice;

public class WitnessHandler : MonoBehaviour
{
    // audioClips contains all the possible voice clips said by witness
    public AudioClip[] audioClips;
    public Oculus.Voice.AppVoiceExperience appVoiceExperience;

    private Canvas listeningIcon;

    void Start() {
        listeningIcon = GetComponentInChildren<Canvas>();
        listeningIcon.enabled = false;
    }


    private void OnTriggerEnter(Collider collider) {
        // start recording
        appVoiceExperience.Activate();
        listeningIcon.enabled = true;
    }
    private void OnTriggerExit(Collider collider) {
        appVoiceExperience.Deactivate();
        listeningIcon.enabled = false;
    }
    private void OnTriggerStay(Collider collider) {
        if (!appVoiceExperience.Active) {
            appVoiceExperience.Activate();
        }
    }

    public static void LogTranscription(string[] text) {
        Debug.Log(string.Join(" ", text));
    }
}
