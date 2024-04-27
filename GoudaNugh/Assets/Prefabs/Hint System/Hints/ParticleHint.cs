using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We will use this hint type to indicate that progression is only possible with the interference of the other player 
public class ParticleHint : Hint
{
    public Color particleColor;
    public ParticleSystem particleSystem;
    public override void DisplayHint() {
        particleSystem.Play();
    }
}