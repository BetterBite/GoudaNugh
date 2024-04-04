using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// please read https://www.nuget.org/packages/OpenAI/ for the documentation of this wrapper class
using OpenAI_API;

public class OpenAIHandler : MonoBehaviour
{
    private OpenAIAPI api = null;
    void Start()
    {
        // The OpenAI api key must be stored in the OPENAI_API_KEY environment variable.
        api = new OpenAIAPI(Environment.GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.User));
    }
    public IEnumerator APIWrapperCallback(MonoBehaviourWithOpenAI obj) {
        while (api == null) yield return null;  // wait for api wrapper to be returned by OpenAI
        obj.SetAPI(api);                        // give this to the MonoBehaviourWithOpenAI
    }
}

public class MonoBehaviourWithOpenAI : MonoBehaviour
{
    [Header("OpenAI Wrapper")]
    public OpenAIHandler openAiHandler;
    protected OpenAIAPI api = null;
    void Start()
    {
        StartCoroutine(openAiHandler.APIWrapperCallback(this)); // on start this class should make a request to the distributor for the openAI wrapper
    }

    /**
    *<summary>This is called by the OpenAIHandler to give the object the OpenAIAPI wrapper. Any setup by the object related to OpenAI's API should be placed here.</summary>
    **/
    public virtual void SetAPI(OpenAIAPI apiFromHandler) { api = apiFromHandler; }
    public bool IsConnected() { return api != null; }
}