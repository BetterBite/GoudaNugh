using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Tutorials.Core.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class RadioScreen : MonoBehaviour
{
    
    public TMP_Text text;
    public GameObject[] screens;
    public RadioVariables.ScreenState activeScreen;

    // Start is called before the first frame update
    void Start()
    {
        SetScreen(RadioVariables.ScreenState.EnterStation);
        //radioScript = radio.GetComponent<PastRadio>();
    }



    public void SetScreen(RadioVariables.ScreenState state)
    {
        activeScreen = state;
        foreach(GameObject screen in screens)
        {
            screen.SetActive(false);
        }
        screens[(int)state].SetActive(true);
    }

    public void DisplayEnteredNumber(int n)
    {
        if (activeScreen == RadioVariables.ScreenState.EnterStation)
        {
            text.text = text.text + n.ToString();
        }

    }

    public void ResetScreenOnWrongCode()
    {
        if (activeScreen == RadioVariables.ScreenState.EnterStation)
        {
            text.text = "Enter Station: ";
        }
    }


    public IEnumerator InvalidateStation()
    {
        Debug.Log("invalid station");
            // Show the GameObject
            SetScreen(RadioVariables.ScreenState.InvalidStation);

            // Wait for 3 seconds
            yield return new WaitForSeconds(0.5f);

            // Hide the GameObject
            SetScreen(RadioVariables.ScreenState.EnterStation);
            ResetScreenOnWrongCode();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
