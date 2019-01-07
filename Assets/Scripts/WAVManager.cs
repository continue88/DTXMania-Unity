using SSTFormat.v4;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WAVManager
{
    public static WAVManager Instance { get; private set; } = new WAVManager();
    private WAVManager() { }

    public void PlaySound(int wavId, ChipType chipType, bool stopAll, MuteGroupType muteGroup, float volume = 1.0f, float delayTime = 0.0f)
    {

    }
}
