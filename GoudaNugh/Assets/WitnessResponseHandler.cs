using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitnessResponseHandler : MonoBehaviour
{
    public void OnFullTranscription(string text) {
        Debug.Log("Transcription complete - "+ text);
    }
}