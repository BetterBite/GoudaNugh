using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Tutorials.Core.Editor;
using UnityEngine;

public class RadioScreen : MonoBehaviour
{
    
    public TMP_Text text;
    public GameObject[] screens;
    public GameObject activeScreen;
    public List<int> code = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        SetScreen(screens[0]);
    }

    public void SetScreen(GameObject c)
    {
        foreach( GameObject screen in screens) { 
            if (c !=  screen)
            {
                screen.SetActive(false);
            }
        }

        c.SetActive(true);
        activeScreen = c;
    }

    public void enterNumber(int number)
    {
        if (activeScreen != screens[0]) return;
        code.Add(number);
        if (code.Count > 2) checkStation();
        text.text = text.text + number;
        Debug.Log(code.Count);
    }

    private void checkStation()
    {
        int[] answer = new int[] { 3, 1, 4 };
        for (int i = 0; i < code.Count; i++)
        {
            if( code[i] != answer[i] )
            {
                Debug.Log("code is wrong");
                code.Clear();
                StartCoroutine(InvalidateStation());
                return;
                
            }
        }
        SetScreen(screens[2]);
    }

    public IEnumerator InvalidateStation()
    {
        Debug.Log("invalid station");
            // Show the GameObject
            SetScreen(screens[1]);

            // Wait for 3 seconds
            yield return new WaitForSeconds(0.5f);

            // Hide the GameObject
            SetScreen(screens[0]);
            text.text = "Enter Station: ";

    }

    private void validateStation()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
