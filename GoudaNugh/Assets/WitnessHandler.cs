using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using Oculus.VoiceSDK.Utilities;
using Meta.Voice;
using Meta.WitAi;
using Oculus.Voice;

public class WitnessHandler : MonoBehaviour
{
    // audioClips contains all the possible voice clips said by witness
    public AudioClip[] audioClips;
    public Oculus.Voice.AppVoiceExperience appVoiceExperience;
    public TextMeshProUGUI subtitle;

    private Canvas listeningIcon;

    void Start() {
        listeningIcon = GetComponentInChildren<Canvas>();
        listeningIcon.enabled = false;
    }


    private void OnTriggerEnter(Collider collider) {
        // start recording
        if (collider.gameObject.GetComponent<Camera>()!=null){
            appVoiceExperience.Activate();
            listeningIcon.enabled = true;
        }
    }
    private void OnTriggerExit(Collider collider) {
        appVoiceExperience.Deactivate();
        listeningIcon.enabled = false;
    }
    private void OnTriggerStay(Collider collider) {
        if (collider.gameObject.GetComponent<Camera>()!=null && !appVoiceExperience.Active) {
            appVoiceExperience.Activate();
        }
    }

    public void SetSubtitle(string text) {
        Debug.Log("Logging transcription:" + text);

        subtitle.text = text;
    }

    public void HandleIntents(string[] values) {
        Debug.Log("WIT output:"+ string.Join(", ", values));
    }
}
