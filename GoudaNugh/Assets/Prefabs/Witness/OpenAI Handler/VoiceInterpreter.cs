using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OpenAI_API;

public class VoiceInterpreter : MonoBehaviourWithOpenAI
{
    // [Header("Events")]
    public event Action<string> FileTranscribed;
    private System.Diagnostics.Stopwatch stopWatch;

    public async void GetTranscriptionFromFile(string filename) {
        stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();

        string resultText = await api.Transcriptions.GetTextAsync(filename);

        stopWatch.Stop();
        Debug.Log("Time to transcribe input: " + stopWatch.Elapsed.TotalMilliseconds);

        FileTranscribed?.Invoke(resultText);    // Invoke relevant event
    }
}
