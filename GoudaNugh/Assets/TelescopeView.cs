using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelescopeView : MonoBehaviour
{
    //public Transform upstairs;    // transform to teleport player to when moving upstairs
    //public Transform player;
    public int imageindex;

    public Image[] blockScreen;
    //public Camera camera;

    void Start()
    {
        imageindex = 0;
        //Color color = blockScreen.color;
        //color.a = 1;
        for (int i = 0; i < 3; i++ )
        {
            blockScreen[i].CrossFadeAlpha(0, 0f, true);
        }
        
    }

    public void ShowImage()
    {
        
        // check that camera can fade to black
        // fade to black
        Debug.Log("In method");
        StartCoroutine(showCoroutine(imageindex));
    }

    public void FadeImage()
    {
        StartCoroutine(fadeCoroutine(imageindex));
    }

    IEnumerator showCoroutine(int i)
    {
        blockScreen[i].CrossFadeAlpha(1, 0.5f, false);
        yield return new WaitForSeconds(0.5f);

    }

    IEnumerator fadeCoroutine(int i)
    {
        blockScreen[i].CrossFadeAlpha(0, 0.5f, false);
        yield return new WaitForSeconds(0.5f);
    }
}
