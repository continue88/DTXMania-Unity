using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSettings
{
    public string Id = "Anonymous";
    public string Name = "Anonymous";
    public double ScrollSpeed = 1.0;
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
    public double MaxRange_Perfect = 0.034;
    public double MaxRange_Great = 0.067;
    public double MaxRange_Good = 0.084;
    public double MaxRange_Ok = 0.117;
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
}
