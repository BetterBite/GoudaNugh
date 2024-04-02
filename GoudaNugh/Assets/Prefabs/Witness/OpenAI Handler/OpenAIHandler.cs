using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OpenAI_API;

public class OpenAIHandler : MonoBehaviour
{
    public int NumberOfDependentManagers = 3;
    private OpenAIAPI api = null;
    // this list could probably be implemented as a FSM as well. VoiceInterpreter -> ChatGPTManager -> TTSManager. Alternatively we just do await TTS(await GPT(await Whisper(input)));
    private List<MonoBehaviourWithOpenAI> subscribers = new List<MonoBehaviourWithOpenAI>();
    void Start()
    {
        api = new OpenAIAPI(Environment.GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.User));
        if (subscribers.Count == NumberOfDependentManagers && api != null) Publish();
    }

    // I'm trying to avoid some synchronisation problems here that i think we just don't get because authenticating with the api will naturally take longer than other local classes subscribing to this class.
    public void Subscribe(MonoBehaviourWithOpenAI obj) {
        if (!subscribers.Contains(obj)) subscribers.Add(obj); // if object is not currently subscribed, add as a suscriber

        // if (subscribers.Count == NumberOfDependentManagers && api != null) Publish();

    }

    private void Publish() {
        foreach (MonoBehaviourWithOpenAI subscriber in subscribers) {
            subscriber.SetAPI(api);
        }
    }
}

public class MonoBehaviourWithOpenAI : MonoBehaviour
{
    public OpenAIHandler openAiHandler;
    protected OpenAIAPI api = null;
    void Start()
    {
        openAiHandler.Subscribe(this);
    }

    public virtual void SetAPI(OpenAIAPI apiFromHandler)
    {
        api = apiFromHandler;
    }
}