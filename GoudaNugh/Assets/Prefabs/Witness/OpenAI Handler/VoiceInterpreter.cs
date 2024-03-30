using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OpenAI_API;

public class VoiceInterpreter : MonoBehaviourWithOpenAI
{
    public string microphoneName = "Default Microphone";


    public override void SetAPI(OpenAIAPI apiFromHandler)
    {
        base.SetAPI(apiFromHandler);
        // GetTextFromAudioStream();
    }

    public async void GetTextFromAudioStream() {
        AudioClip clip = Microphone.Start(Microphone.devices[0], true, 10, 44100);
        SaveByteArrayToWAV(ConvertAudioClipToByteArray(clip),"speech.wav", clip);

        string transcription = await api.Transcriptions.GetTextAsync("speech.wav");
        Debug.Log("Transcription: "+transcription);
        // return transcription;
    }

    private static byte[] ConvertAudioClipToByteArray(AudioClip audioClip)
    {
        float[] samples = new float[audioClip.samples * audioClip.channels];
        audioClip.GetData(samples, 0);

        // Convert float samples to byte array
        byte[] byteArray = new byte[samples.Length * 2]; // 2 bytes per sample for Unity AudioClip
        int rescaleFactor = 32767; // To convert float to short

        for (int i = 0; i < samples.Length; i++)
        {
            short intSample = (short)(samples[i] * rescaleFactor);
            byteArray[i * 2] = (byte)(intSample & 0xFF);
            byteArray[i * 2 + 1] = (byte)((intSample >> 8) & 0xFF);
        }

        return byteArray;
    }

    private static void SaveByteArrayToWAV(byte[] audioData, string filePath, AudioClip audioClip)
    {
        // Write the audioData to a WAV file
        FileStream fileStream = new FileStream(filePath, FileMode.Create);
        BinaryWriter writer = new BinaryWriter(fileStream);

        // Write WAV header
        writer.Write(new char[4] { 'R', 'I', 'F', 'F' });
        writer.Write(audioData.Length + 36);
        writer.Write(new char[4] { 'W', 'A', 'V', 'E' });
        writer.Write(new char[4] { 'f', 'm', 't', ' ' });
        writer.Write(16); // PCM format
        writer.Write((short)1); // Audio format (1 for PCM)
        writer.Write((short)audioClip.channels); // Number of channels
        writer.Write(audioClip.frequency); // Sample rate
        writer.Write(audioClip.frequency * audioClip.channels * 2); // Byte rate
        writer.Write((short)(audioClip.channels * 2)); // Block align
        writer.Write((short)16); // Bits per sample
        writer.Write(new char[4] { 'd', 'a', 't', 'a' });
        writer.Write(audioData.Length);

        // Write audioData
        writer.Write(audioData);

        writer.Close();
        fileStream.Close();
    }
}
