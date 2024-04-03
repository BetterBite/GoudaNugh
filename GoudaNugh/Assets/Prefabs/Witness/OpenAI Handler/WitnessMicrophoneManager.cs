using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WitnessMicrophoneManager : MonoBehaviour
{
    // https://docs.unity3d.com/ScriptReference/Microphone.html for Microphone docs
    [Header("Microphone Settings")]
    private string microphoneName;
    public int recordLength = 5;
    public bool loopRecord = true;
    public int sampleRate = 44100;
    public float thresholdSilenceTime = 0.1f;

    // [Header("Events")]
    public event Action<string> RecordingSaved;
    public event Action RecordingStarted;
    public event Action RecordingStopped;

    private bool isRecording = false;
    private AudioClip clip = null;
    private float lastTime = 0f;

    void Start()
    {
        microphoneName = Microphone.devices[0]; // select first available microphone
    }

    public void StartRecording() {
        Debug.Log("Started Recording!");
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
        SaveWav.Save("C:/Users/jacob/Documents/GoudaNugh/GoudaNugh/speech.wav", clip);
        RecordingSaved?.Invoke("speech.wav");
    } 

    void Update()
    {
        if (isRecording) {
            if (clip != null && Microphone.GetPosition(null) > 0 && IsSilent()) {   // check if quiet enough to end recording
                if (Time.time - lastTime > thresholdSilenceTime) StopRecording();                   // ensure it has been quiet for threshold time
            }
            else lastTime = Time.time;                                              // if it's not quiet enough, reset threshold time
        }
    }

    // helpers
    private bool IsSilent()
    {

        int sampleWindow = 128; 
        float[] samples = new float[sampleWindow];
        int microphonePosition = Microphone.GetPosition(null) - sampleWindow + 1;
        if (microphonePosition < 0) return false;
        clip.GetData(samples, microphonePosition);
        float averageLevel = GetAverageVolume(samples);
        float threshold = 0.05f; 
        return averageLevel < threshold;
    }
    private float GetAverageVolume(float[] samples)
    {
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
        var filepath = Path.Combine(Application.persistentDataPath, filename);
        var samples = new float[clip.samples];
        clip.GetData(samples, 0);
        var waveData = ConvertAndWrite(samples, clip.channels, clip.frequency);
        WriteHeader(filepath, waveData, clip.channels, clip.frequency);
        return true;
    }
    static byte[] ConvertAndWrite(float[] samples, int channels, int frequency)
    {
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
    static void WriteHeader(string filepath, byte[] waveData, int channels, int frequency)
    {
        var fileStream = new FileStream(filepath, FileMode.Create);
        var memoryStream = new MemoryStream();
        memoryStream.Write(waveData, 0, waveData.Length);
        var fileLength = memoryStream.Length;
        fileStream.Seek(0, SeekOrigin.Begin);
        var riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
        fileStream.Write(riff, 0, 4);
        var chunkSize = BitConverter.GetBytes(fileLength - 8);
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
        var subChunk2 = BitConverter.GetBytes(fileLength - HEADER_SIZE);
        fileStream.Write(subChunk2, 0, 4);
        memoryStream.Seek(0, SeekOrigin.Begin);
        memoryStream.CopyTo(fileStream);
        fileStream.Close();
    }
}
