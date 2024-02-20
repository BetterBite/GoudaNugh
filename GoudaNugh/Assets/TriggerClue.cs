using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerClue : MonoBehaviour
{
    public AudioSource audioSource;
    public void PlayAudio() {
        audioSource.Play();
    }
}
