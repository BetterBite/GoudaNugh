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
    public AudioSource audioSource;
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

        if (values[0] == "info_about_self" && values.Length > 1) {
            if (values[1] == "name") {
                // play name audio
                PlayAudio(audioClips[0]);
            } else if (values[3] == "victim") {
                // play found victim audio
                PlayAudio(audioClips[1]);
            } else if (values[2] == "alibi") {
                // play alibi audio
                PlayAudio(audioClips[2]);
            } else if (values[1] == "thomas") {
                PlayAudio(audioClips[4]);
            } else if (values[1] == "lurni") {
                PlayAudio(audioClips[5]);
            } else if (values[1] == "walters") {
                PlayAudio(audioClips[6]);
            }
        } else if (values[0] == "info_about_item" && values.Length > 1) {
            if (values[1] == "figurine") {
                // play figurine audio
                PlayAudio(audioClips[3]);
            } else if (values[2] == "clock") {
                // play clock audio
                PlayAudio(audioClips[4]);
            } else if (values[3] == "globe") {
                // play globe audio
                PlayAudio(audioClips[5]);
            } else if (values[4] == "telescope") {
                // play telescope audio
                PlayAudio(audioClips[6]);
            }
        }        
    }

    private void PlayAudio(AudioClip clip) {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
