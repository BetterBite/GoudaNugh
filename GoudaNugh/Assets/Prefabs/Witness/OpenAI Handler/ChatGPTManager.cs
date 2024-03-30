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

    private System.Diagnostics.Stopwatch stopWatch;

    public override void SetAPI(OpenAIAPI apiFromHandler) {
        base.SetAPI(apiFromHandler);

        stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();

        SetupChat();
        GetResponseFromPrompt("Hello, who are you?");
        // ttsManager.ConvertTextToSpeech("I am a witness to the murder");
    }

    private void SetupChat() {
        chat = api.Chat.CreateConversation();
        // chat.Model = Model.GPT4_Turbo;
        chat.Model = Model.ChatGPTTurbo;
        chat.RequestParameters.Temperature = 0;
        chat.AppendSystemMessage(SystemPrompt); // give chat the system prompt
    }

    public async void GetResponseFromPrompt(string prompt) {
        if (chat == null) {
            // throw error
            return;
        }

        chat.AppendUserInput(prompt);

        string response = await chat.GetResponseFromChatbotAsync();
        Debug.Log("Response from Chat GPT: " + response);

        // for benchmarking
        stopWatch.Stop();
        Debug.Log(stopWatch.Elapsed.TotalMilliseconds);

        // convert this response to speech
        ttsManager.ConvertTextToSpeech(response);
    }
}
