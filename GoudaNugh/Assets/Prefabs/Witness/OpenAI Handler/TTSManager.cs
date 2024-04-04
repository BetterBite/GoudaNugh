using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


using OpenAI_API;
using OpenAI_API.Audio;
using OpenAI_API.Models;

public class TTSManager : MonoBehaviourWithOpenAI
{
    [Header("Text-to-Speech Settings")]
    public string voice = TextToSpeechRequest.Voices.Onyx;
    private AudioSource audioSource;
    public event Action TextConverted;

    private System.Diagnostics.Stopwatch stopWatch;

    void Start() {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    public async void ConvertTextToSpeech(string text) {
        Debug.Log("Converting prompt to speech: "+text);

        // for benchmarking
        stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();

        var request = new TextToSpeechRequest()
        {
            Input = text,
            ResponseFormat = TextToSpeechRequest.ResponseFormats.MP3,
            Model = Model.TTS_Speed,
            Voice = voice,
            Speed = 1.1
        };
        // Possibly implement using GetSpeechAsStreamAsync to avoid the need for file handling (to be honest I think this maybe save a handful of milliseconds. Making API calls is the limiting factor by far)
        await api.TextToSpeech.SaveSpeechToFileAsync(request, $"{Application.persistentDataPath}/testTTS.mp3");
        StartCoroutine(GetAudioClip("testTTS.mp3"));
        
    }

    IEnumerator GetAudioClip(string path)
    {
        // TODO: fix the absolute filepaths
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip($"file://{Application.persistentDataPath}/{path}", AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError) Debug.Log(www.error);
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                TextConverted?.Invoke();

                audioSource.clip = myClip;
                audioSource.Play();

                // for benchmarking
                stopWatch.Stop();
                Debug.Log("Time to convert response to speech: " + stopWatch.Elapsed.TotalMilliseconds);
            }
        }
    }
}