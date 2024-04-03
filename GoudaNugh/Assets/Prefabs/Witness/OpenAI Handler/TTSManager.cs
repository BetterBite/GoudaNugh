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
    public string voice = TextToSpeechRequest.Voices.Nova;
    public AudioSource audioSource;
    // [Header("Events")]
    public event Action TextConverted;

    private System.Diagnostics.Stopwatch stopWatch;

    public async void ConvertTextToSpeech(string text) {
        Debug.Log("Converting prompt to speech: "+text);

        // for benchmarking
        stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();

        var request = new TextToSpeechRequest()
        {
            Input = text,
            ResponseFormat = TextToSpeechRequest.ResponseFormats.MP3,
            Model = Model.DefaultTTSModel,
            Voice = "onyx",
            Speed = 1.1
        };
        // TODO: Implement using GetSpeechAsStreamAsync to avoid the need for file handling (to be honest I think this maybe save a handful of milliseconds. Making API calls is the limiting factor by far)
        await api.TextToSpeech.SaveSpeechToFileAsync(request, "testTTS.mp3");
        StartCoroutine(GetAudioClip("testTTS.mp3"));
        
    }

    IEnumerator GetAudioClip(string path)
    {
        // TODO: fix the absolute filepaths
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip($"file://C:/Users/jacob/Documents/GoudaNugh/GoudaNugh/{path}", AudioType.MPEG))
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