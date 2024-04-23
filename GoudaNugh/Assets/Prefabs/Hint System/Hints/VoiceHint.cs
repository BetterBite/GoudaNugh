using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceHint : Hint
{
    public TTSManager ttsManager;
    [TextArea(4,10)]
    public string hintAsText;
    public override void DisplayHint() {
        ttsManager.ConvertTextToSpeech(hintAsText);
        // rememer ttsManager.TextConverted is an event we can subscribe to if necessary
    }
}
