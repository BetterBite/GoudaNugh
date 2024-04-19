using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitnessManager : MonoBehaviour
{
    private WitnessMicrophoneManager witnessMicrophoneManager;
    private VoiceInterpreter voiceInterpreter;
    private ChatGPTManager chatGPTManager;
    private TTSManager ttsManager;

    private bool InProgress = false;

    void Start() {
        witnessMicrophoneManager = GetComponentInChildren<WitnessMicrophoneManager>();
        voiceInterpreter = GetComponentInChildren<VoiceInterpreter>();
        chatGPTManager = GetComponentInChildren<ChatGPTManager>();
        ttsManager = GetComponentInChildren<TTSManager>();
    }
    public void StartWitnessQuery() {
        if (!voiceInterpreter.IsConnected()) throw new System.InvalidOperationException("Cannot start a witness query as the voice interpreter is not yet connected to OpenAI");
        if (!chatGPTManager.IsConnected()) throw new System.InvalidOperationException("Cannot start a witness query as the chat GPT manager is not yet connected to OpenAI");
        if (!ttsManager.IsConnected()) throw new System.InvalidOperationException("Cannot start a witness query as the text-to-speech manager is not yet connected to OpenAI");
        
        if (!chatGPTManager.IsSetup()) throw new System.InvalidOperationException("Cannot start a witness query as the chat GPT manager has not finished setting up");

        if (InProgress) return; // if a query is in progress, do not allow new queries (for now)

        InProgress = true;
        witnessMicrophoneManager.RecordingSaved += OnRecordingSaved;
        witnessMicrophoneManager.StartRecording();
    }

    private void OnRecordingSaved(string filename) {
        witnessMicrophoneManager.RecordingSaved -= OnRecordingSaved;

        // InProgress = false;
        voiceInterpreter.FileTranscribed += OnFileTranscribed;
        voiceInterpreter.GetTranscriptionFromFile(filename);
    }
    private void OnFileTranscribed(string transcription) {
        voiceInterpreter.FileTranscribed -= OnFileTranscribed;

        chatGPTManager.ResponseReceived += OnResponseReceived;
        chatGPTManager.GetResponseFromPrompt(transcription);
    }
    private void OnResponseReceived(string response) {
        chatGPTManager.ResponseReceived -= OnResponseReceived;

        ttsManager.TextConverted += OnTextConverted;
        ttsManager.ConvertTextToSpeech(response);
    }
    private void OnTextConverted() {
        ttsManager.TextConverted -= OnTextConverted;

        InProgress = false;
    }
}