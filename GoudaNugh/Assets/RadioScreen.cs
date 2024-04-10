using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RadioScreen : MonoBehaviour
{
    public TMP_Text text;
    public Canvas[] screens;
    public Canvas activeScreen;
    public List<int> code = new List<int>();
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetScreen(Canvas c)
    {
        foreach( Canvas screen in screens) { 
            if (c !=  screen)
            {
                screen.enabled = false;
            }
        }

        c.enabled = true;
        activeScreen = c;
    }

    public void enterNumber(string number)
    {
        code.Add(int.Parse(number));
        if (code.Count > 2) checkStation();
        text.text = text.text + number;
        Debug.Log(code.Count);
    }

    private void checkStation()
    {
        int[] answer = new int[] { 3, 1, 4 };
        for (int i = 0; i < answer.Length; i++)
        {
            if( code[i] != answer[i] )
            {
                Debug.Log("code is wrong");
                code.Clear();
                invalidateStation();
                
            }
        }
    }

    private IEnumerator invalidateStation()
    {
        Debug.Log("invalid station");
            // Show the GameObject
            SetScreen(screens[1]);

            // Wait for 3 seconds
            yield return new WaitForSeconds(2f);

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
