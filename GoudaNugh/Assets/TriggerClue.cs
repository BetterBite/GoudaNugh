using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace System {
public class TriggerClue : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private Dictionary<string, int> keywordAudioMap = new Dictionary<string, int>{};
    private Dictionary<int, bool> audioPossibleMap = new Dictionary<int, bool>{};

    void Start() {
        string[] keyword = {
            "hello",
            "know",
            "see",
            "code",
            "clock",
            "",
            ""
        };

        for (int i=0 ; i < keyword.Length ; i++) {
            keywordAudioMap.Add(keyword[i], i);
            audioPossibleMap.Add(i, true);
        }

    }


    public AudioClip GetAudioClipFromKeywords(string[] words) {

        foreach (string word in words) {
            if (!keywordAudioMap.ContainsKey(word)) continue;

            int index = keywordAudioMap[word];
            if (audioPossibleMap[index]) {
                audioPossibleMap[index] = false;
                return audioClips[index];
            }
        }
        return audioClips[7];
    }

    public void HandleTranscription(string text) {
        Debug.Log(text);

        string[] words = text.ToLower().Split(' ');

        AudioClip clip = GetAudioClipFromKeywords(words);
        PlayAudio(clip);
    }

    public void PlayAudio(AudioClip audioClip) {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
}