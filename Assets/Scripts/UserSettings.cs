using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSettings
{
    public string Id = "Anonymous";
    public string Name = "Anonymous";
    public float ScrollSpeed = 1.0f;
    public bool Fullscreen = false;
    public bool AutoPlay_LeftCymbal = true;
    public bool AutoPlay_HiHat = true;
    public bool AutoPlay_LeftPedal = true;
    public bool AutoPlay_Snare = true;
    public bool AutoPlay_Bass = true;
    public bool AutoPlay_HighTom = true;
    public bool AutoPlay_LowTom = true;
    public bool AutoPlay_FloorTom = true;
    public bool AutoPlay_RightCymbal = true;
    public float MaxRange_Perfect = 0.034f;
    public float MaxRange_Great = 0.067f;
    public float MaxRange_Good = 0.084f;
    public float MaxRange_Ok = 0.117f;
    public bool CymbalFree = true;
    public PlayMode PlayMode = 0;
    public int RideLeft = 0;
    public int ChinaLeft = 0;
    public int SplashLeft = 1;
    public bool DrumSound = true;
    public string LaneType = "TypeA";
    public int LaneTrans = 50;
    public DrumChipPropertyManager DrumChipProperty = new DrumChipPropertyManager(
        PlayMode.BASIC,
        new LeftAndRightDisplayTrack { ChinaLeft = false, RideLeft = false, SplashLeft = true },
        InputPresetType.Basic);

    public bool AutoPlay(AutoPlayType type)
    {
        switch (type)
        {
            case AutoPlayType.LeftCrash: return AutoPlay_LeftCymbal;
            case AutoPlayType.HiHat: return AutoPlay_HiHat;
            case AutoPlayType.Foot: return AutoPlay_LeftPedal;
            case AutoPlayType.Snare: return AutoPlay_Snare;
            case AutoPlayType.Bass: return AutoPlay_Bass;
            case AutoPlayType.Tom1: return AutoPlay_HighTom;
            case AutoPlayType.Tom2: return AutoPlay_LowTom;
            case AutoPlayType.Tom3: return AutoPlay_FloorTom;
            case AutoPlayType.RightCrash: return AutoPlay_RightCymbal;
            default: return true;
        }
    }
    public bool AutoPlayAllOn()
    {
        return AutoPlay_LeftCymbal &&
            AutoPlay_HiHat &&
            AutoPlay_LeftPedal &&
            AutoPlay_Snare &&
            AutoPlay_Bass &&
            AutoPlay_HighTom &&
            AutoPlay_LowTom &&
            AutoPlay_FloorTom &&
            AutoPlay_RightCymbal;
    }

    public float MaxHitTime(JudgmentType type)
    {
        switch (type)
        {
            case JudgmentType.PERFECT: return MaxRange_Perfect;
            case JudgmentType.GREAT: return MaxRange_Great;
            case JudgmentType.GOOD: return MaxRange_Good;
            case JudgmentType.OK: return MaxRange_Ok;
            default: return 0;
        }
    }
}
