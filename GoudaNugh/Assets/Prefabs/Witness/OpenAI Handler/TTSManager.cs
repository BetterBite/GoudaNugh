using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


using OpenAI_API;
using OpenAI_API.Audio;
using OpenAI_API.Models;

public class TTSManager : MonoBehaviourWithOpenAI
{
    public string voice = TextToSpeechRequest.Voices.Nova;
    public AudioSource audioSource;

    private System.Diagnostics.Stopwatch stopWatch;

    public async void ConvertTextToSpeech(string text) {
        Debug.Log("Converting prompt to speech: "+text);

        // for benchmarking
        stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();

        // using (Stream result = await api.TextToSpeech.GetSpeechAsStreamAsync(text, "nova"))
        // using (StreamReader reader = new StreamReader(result))
        // {
        //     byte[] bytes = Encoding.UTF8.GetBytes(reader.ReadToEnd());

        //     // float[] samples = new float[bytes.Length / 2];

        //     // for (int i = 0; i < bytes.Length / 2; i++)
        //     //     samples[i] = System.BitConverter.ToSingle(bytes, i * 2);

        //     float[] samples = new float[bytes.Length / 2]; //size of a float is 4 bytes
        //     System.Buffer.BlockCopy(bytes, 0, samples, 0, bytes.Length);
            
        //     int channels = 1; //Assuming audio is mono because microphone input usually is
        //     int sampleRate = 24000; //Assuming your samplerate is 44100 or change to 48000 or whatever is appropriate
            
        //     AudioClip clip = AudioClip.Create("SpeechFromTTS", samples.Length, channels, sampleRate, false);
        //     clip.SetData(samples, 0);

        //     audioSource.clip = clip;
        // }

        var request = new TextToSpeechRequest()
        {
            Input = text,
            ResponseFormat = TextToSpeechRequest.ResponseFormats.MP3,
            Model = Model.DefaultTTSModel,
            Voice = "onyx",
            Speed = 1.1
        };
        // TODO: Implement using GetSpeechAsStreamAsync to avoid the need for file handling
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
                audioSource.clip = myClip;
                audioSource.Play();

                // for benchmarking
                stopWatch.Stop();
                Debug.Log("Time to convert response to speech: " + stopWatch.Elapsed.TotalMilliseconds);
            }
        }
    }
}