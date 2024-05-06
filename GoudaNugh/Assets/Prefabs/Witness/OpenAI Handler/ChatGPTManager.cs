using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;

public class ChatGPTManager : MonoBehaviourWithOpenAI
{
    [Header("Chat GPT Settings")]
    [TextArea(10,4)]
    public string SystemPrompt;
    
    public event Action<string> ResponseReceived;
    private Conversation chat = null;
    private bool setup = false;
    private System.Diagnostics.Stopwatch stopWatch;

    public override void SetAPI(OpenAIAPI apiFromHandler) {
        base.SetAPI(apiFromHandler);
        SetupChat();    // set up the chat conversation with open ai
    }
    public bool IsSetup() { return setup != false; }

    private void SetupChat() {
        chat = api.Chat.CreateConversation();
        chat.Model = Model.ChatGPTTurbo;    // Quicker model than Model.GPT4_Turbo?
        chat.RequestParameters.Temperature = 0;
        chat.AppendSystemMessage(SystemPrompt); // give chat the system prompt
        setup = true;
    }

    public async void GetResponseFromPrompt(string prompt) {
        if (chat == null) throw new System.InvalidOperationException("Cannot make a query to gpt before initialising ChatGPTManager.chat");

        stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();

        chat.AppendUserInput(prompt);
        string response = await chat.GetResponseFromChatbotAsync();

        // for benchmarking
        stopWatch.Stop();
        Debug.Log("Time to process transcription with gpt: " + stopWatch.Elapsed.TotalMilliseconds);

        ResponseReceived?.Invoke(response);
    }
}
