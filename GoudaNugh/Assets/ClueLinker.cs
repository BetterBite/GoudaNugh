using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueLinker : MonoBehaviour
{
    public static ClueLinker Singleton;

    private void Awake()
    {
        Singleton = this;
    }
    public enum Links
    {
        Moon,
        Sun,
        CabinetKey,

    }
}
