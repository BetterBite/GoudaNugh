using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportUpstairs : MonoBehaviour
{
    public Transform upstairs;    // transform to teleport player to when moving upstairs
    public Transform player;

    public Image blockScreen;
    public Camera camera;

    void Start() {
        Color color = blockScreen.color;
        color.a = 1;
        blockScreen.CrossFadeAlpha(0,0f, true);
    }

    public void teleportTo(Transform transform) {
        // check that camera can fade to black
        // fade to black

        StartCoroutine(fadeCoroutine(transform));
    }

    IEnumerator fadeCoroutine(Transform transform) {
        blockScreen.CrossFadeAlpha(1, 0.5f, false);
        yield return new WaitForSeconds(0.5f);

        // move player to transform
        player.position = upstairs.position;

        // fade from black
        blockScreen.CrossFadeAlpha(0, 0.5f, false); 
    }
}
