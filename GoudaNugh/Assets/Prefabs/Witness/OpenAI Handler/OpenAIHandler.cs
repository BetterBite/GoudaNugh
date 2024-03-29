using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OpenAI_API;

public class OpenAIHandler : MonoBehaviour
{
    public int NumberOfDependentManagers = 1;
    private OpenAIAPI api = null;
    private List<MonoBehaviourWithOpenAI> subscribers = new List<MonoBehaviourWithOpenAI>();
    void Start()
    {
        api = new OpenAIAPI(Environment.GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.User));
        if (subscribers.Count == NumberOfDependentManagers && api != null) Broadcast();
    }

    
    public void Subscribe(MonoBehaviourWithOpenAI obj) {
        if (!subscribers.Contains(obj)) subscribers.Add(obj); // if object is not currently subscribed, add as a suscriber

        // if (subscribers.Count == NumberOfDependentManagers && api != null) Broadcast();

    }

    private void Broadcast() {
        foreach (MonoBehaviourWithOpenAI subscriber in subscribers) {
            subscriber.SetAPI(api);
        }
    }
}

public abstract class MonoBehaviourWithOpenAI : MonoBehaviour
{
    public abstract void SetAPI(OpenAIAPI apiFromHandler);
}