using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportUpstairs : MonoBehaviour
{
    public Transform upstairs;    // transform to teleport player to when moving upstairs
    public Transform player;

    public FadeScreen fadeScreen;

    public void teleportTo(Transform transform)
    {
        StartCoroutine(fadeCoroutine(transform));
    }

    IEnumerator fadeCoroutine(Transform transform)
    {
        fadeScreen.Fade(0, 1);
        yield return new WaitForSeconds(fadeScreen.FadeDuration);

        // move player to transform
        //player.position = upstairs.position;
        // Create a new Vector3 for the player's position with x and z from player.position and y from upstairs.position
        Vector3 newUpstairs = new Vector3(player.position.x, upstairs.position.y, player.position.z);

        // Move player to the new position
        player.position = newUpstairs;

        // fade from black
        fadeScreen.Fade(1, 0);
    }
}

