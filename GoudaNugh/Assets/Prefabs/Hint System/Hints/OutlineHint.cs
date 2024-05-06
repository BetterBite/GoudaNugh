using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OutlineHint : Hint
{
    private Outline outline;
    [Header("Outline Settings")]
    public Color OutlineColor = Color.white;
    public float OutlineThickness = 5f;
    void Awake() {
        outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = OutlineColor;
        outline.OutlineWidth = OutlineThickness;
        outline.enabled = false;
    }
    public override void DisplayHint() {
        StartCoroutine(ShowAndHideOutline());
    }

    IEnumerator ShowAndHideOutline() {
        outline.enabled = true;

        yield return new WaitForSeconds(5);

        outline.enabled = false;
    }
}
