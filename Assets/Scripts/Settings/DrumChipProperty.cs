using SSTFormat.v4;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumChipProperty
{
    public ChipType ChipType = ChipType.Unknown;
    public DrumType DrumType = DrumType.Unknown;
    public DisplayTrackType DisplayTrackType = DisplayTrackType.Unknown;
    public DisplayChipType DisplayChipType = DisplayChipType.Unknown;
    public DrumInputType DrumInputType = DrumInputType.Unknown;
    public AutoPlayType AutoPlayType = AutoPlayType.Unknown;
    public InputGroupType InputGroupType = InputGroupType.Unknown;
    public bool MuteBeforeUtter = false;
    public MuteGroupType MuteGroupType = MuteGroupType.Unknown;
    public bool AutoPlayON_AutoHitSound = false;
    public bool AutoPlayON_AutoHitHide = false;
    public bool AutoPlayON_AutoJudge = false;
    public bool AutoPlayON_MissJudge = false;
    public bool AutoPlayOFF_AutoHitSound = false;
    public bool AutoPlayOFF_AutoHitHide = false;
    public bool AutoPlayOFF_AutoHitJudge = false;
    public bool AutoPlayOFF_UserHitSound = false;
    public bool AutoPlayOFF_UserHitHide = false;
    public bool AutoPlayOFF_UserHitJudge = false;
    public bool AutoPlayOFF_MissJudge = false;
}
