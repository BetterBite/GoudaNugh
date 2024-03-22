using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{

    public float FadeDuration;
    public Color FadeColour;
    public Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.SetColor("_BaseColor", FadeColour);
    }

    public void Fade(float alphaIn, float alphaOut) {
        StartCoroutine(FadeCoroutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeCoroutine(float alphaIn, float alphaOut) {
        float timer = 0;
        Color newColour = FadeColour;

        while (timer < FadeDuration) {
            newColour.a = Mathf.Lerp(alphaIn, alphaOut, timer / FadeDuration);
            rend.material.SetColor("_BaseColor", newColour);

            timer += Time.deltaTime;
            yield return null;
        }

        newColour.a = alphaOut;
        rend.material.SetColor("_BaseColor", newColour);
    }
}
