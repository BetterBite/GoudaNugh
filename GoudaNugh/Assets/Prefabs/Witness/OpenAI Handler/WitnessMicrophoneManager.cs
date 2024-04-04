using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WitnessMicrophoneManager : MonoBehaviour
{
    // https://docs.unity3d.com/ScriptReference/Microphone.html for Microphone docs
    [Header("Microphone Settings")]
    public int recordLength = 5;
    public bool loopRecord = true;
    public int sampleRate = 44100;

    public event Action<string> RecordingSaved;
    public event Action RecordingStarted;
    public event Action RecordingStopped;

    private string microphoneName;

    private bool isRecording = false;
    private AudioClip clip = null;
    private float lastTime = 0f;

    void Start() {
        microphoneName = Microphone.devices[0]; // select first available microphone
    }

    public void StartRecording() {
        Debug.Log("Started Recording! " + microphoneName);
        clip = Microphone.Start(microphoneName, loopRecord, recordLength, sampleRate);
        isRecording = true;
        lastTime = Time.time;

        RecordingStarted?.Invoke(); // invoke relevant event
    }

    public void StopRecording() {
        if (!isRecording) return;   // Can't stop recording if not already recording.

        Microphone.End(null);
        isRecording = false;

        RecordingStopped?.Invoke(); // invoke relevant event
        
        Debug.Log("Stopped Recording.");
        SaveRecording();
    }

    private void SaveRecording() {
        SaveWav.Save("speech.wav", clip);
        RecordingSaved?.Invoke("speech.wav");
    } 

    void Update()
    {
        if (isRecording) {
            if (clip != null && Microphone.GetPosition(null) > 0 && IsSilent()) {   // check if quiet enough to end recording
                if (Time.time - lastTime > 0.5f) StopRecording();                   // ensure it has been quiet for threshold time
            }
            else {
                lastTime = Time.time;
            }                                              // if it's not quiet enough, reset threshold time
        }
    }

    // helpers
    private bool IsSilent() {

        int sampleWindow = 128; 
        float[] samples = new float[sampleWindow];
        int microphonePosition = Microphone.GetPosition(null) - sampleWindow + 1;
        if (microphonePosition < sampleRate) return false;
        clip.GetData(samples, microphonePosition);
        float averageLevel = GetAverageVolume(samples);
        float threshold = 0.05f; 
        return averageLevel < threshold;
    }
    private float GetAverageVolume(float[] samples) {
        float sum = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += Mathf.Abs(samples[i]);
        }
        return sum / samples.Length;
    }
}

public static class SaveWav
{
    const int HEADER_SIZE = 44;

    public static bool Save(string filename, AudioClip clip)
    {
        try
        {
            var filepath = Path.Combine(Application.persistentDataPath, filename);
            var samples = new float[clip.samples];
            clip.GetData(samples, 0);
            var waveData = ConvertAndWrite(samples);
            WriteHeader(filepath, waveData, clip.channels, clip.frequency);
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error saving WAV file: " + ex.Message);
            return false;
        }
    }

    static byte[] ConvertAndWrite(float[] samples)
    {
        const int BYTES_PER_SAMPLE = 2;
        const int RESCALE_FACTOR = 32767;

        byte[] byteArray = new byte[samples.Length * BYTES_PER_SAMPLE];

        for (int i = 0; i < samples.Length; i++)
        {
            short intSample = (short)(samples[i] * RESCALE_FACTOR);
            byteArray[i * BYTES_PER_SAMPLE] = (byte)(intSample & 0xFF);
            byteArray[i * BYTES_PER_SAMPLE + 1] = (byte)((intSample >> 8) & 0xFF);
        }

        return byteArray;
    }

    static void WriteHeader(string filepath, byte[] waveData, int channels, int frequency)
    {
        using (var fileStream = new FileStream(filepath, FileMode.Create))
        {
            var riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
            fileStream.Write(riff, 0, 4);
            var chunkSize = BitConverter.GetBytes(waveData.Length + HEADER_SIZE - 8);
            fileStream.Write(chunkSize, 0, 4);
            var wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
            fileStream.Write(wave, 0, 4);
            var fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
            fileStream.Write(fmt, 0, 4);
            var subChunk1 = BitConverter.GetBytes(16);
            fileStream.Write(subChunk1, 0, 4);
            var audioFormat = BitConverter.GetBytes((short)1);
            fileStream.Write(audioFormat, 0, 2);
            var numChannels = BitConverter.GetBytes((short)channels);
            fileStream.Write(numChannels, 0, 2);
            var sampleRate = BitConverter.GetBytes(frequency);
            fileStream.Write(sampleRate, 0, 4);
            var byteRate = BitConverter.GetBytes(frequency * channels * 2);
            fileStream.Write(byteRate, 0, 4);
            var blockAlign = BitConverter.GetBytes((short)(channels * 2));
            fileStream.Write(blockAlign, 0, 2);
            var bitsPerSample = BitConverter.GetBytes((short)16);
            fileStream.Write(bitsPerSample, 0, 2);
            var dataString = System.Text.Encoding.UTF8.GetBytes("data");
            fileStream.Write(dataString, 0, 4);
            var subChunk2 = BitConverter.GetBytes(waveData.Length);
            fileStream.Write(subChunk2, 0, 4);
            fileStream.Write(waveData, 0, waveData.Length);
        }
    }
}
