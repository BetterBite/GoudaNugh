using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;

public class ChatGPTManager : MonoBehaviourWithOpenAI
{
    public string SystemPrompt;
    private Conversation chat = null;
    public TTSManager ttsManager;
    public string DebugPrompt = "Are you the murderer?";

    private System.Diagnostics.Stopwatch stopWatch;

    public override void SetAPI(OpenAIAPI apiFromHandler) {
        base.SetAPI(apiFromHandler);

        SetupChat();

        // start benchmarking after setup because realistically the setup isn't what we're optimising.
        // stopWatch = new System.Diagnostics.Stopwatch();
        // stopWatch.Start();

        // GetResponseFromPrompt(DebugPrompt);
        // ttsManager.ConvertTextToSpeech("I am a witness to the murder");
    }

    private void SetupChat() {
        chat = api.Chat.CreateConversation();
        // chat.Model = Model.GPT4_Turbo;
        chat.Model = Model.ChatGPTTurbo;    // Quicker model?
        chat.RequestParameters.Temperature = 0;
        chat.AppendSystemMessage(SystemPrompt); // give chat the system prompt
    }

    public async void GetResponseFromPrompt(string prompt) {
        if (chat == null) {
            // TODO: throw error
            return;
        }
        stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();

        chat.AppendUserInput(prompt);

        string response = await chat.GetResponseFromChatbotAsync();
        Debug.Log("Response from Chat GPT: " + response);

        // for benchmarking
        stopWatch.Stop();
        Debug.Log("Time to process transcription with gpt: " + stopWatch.Elapsed.TotalMilliseconds);

        // convert this response to speech
        ttsManager.ConvertTextToSpeech(response);
    }
}
