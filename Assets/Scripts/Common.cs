using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayMode : int
{
    BASIC = 0,
    EXPERT = 1,
}

public struct LeftAndRightDisplayTrack
{
    public bool RideLeft { get; set; }
    public bool ChinaLeft { get; set; }
    public bool SplashLeft { get; set; }
}

public enum InputPresetType
{
    CymbalFree,
    Basic,
}

public enum DisplayTrackType
{
    Unknown,
    LeftCymbal,
    HiHat,
    Foot,   // 左ペダル
    Snare,
    Bass,
    Tom1,
    Tom2,
    Tom3,
    RightCymbal,
}

public enum DisplayChipType
{
    Unknown,
    LeftCymbal,
    RightCymbal,
    HiHat,
    HiHat_Open,
    HiHat_HalfOpen,
    Foot,
    LeftPedal,
    Snare,
    Snare_OpenRim,
    Snare_ClosedRim,
    Snare_Ghost,
    Bass,
    LeftBass,
    Tom1,
    Tom1_Rim,
    Tom2,
    Tom2_Rim,
    Tom3,
    Tom3_Rim,
    LeftRide,
    RightRide,
    LeftRide_Cup,
    RightRide_Cup,
    LeftChina,
    RightChina,
    LeftSplash,
    RightSplash,
    LeftCymbal_Mute,
    RightCymbal_Mute,
}

public enum DrumInputType
{
    Unknown,
    LeftCrash,
    Ride,
    //Ride_Cup,			--> Ride として扱う。（打ち分けない。）
    China,
    Splash,
    HiHat_Open,
    //HiHat_HalfOpen,	--> HiHat_Open として扱う。（打ち分けない。）
    HiHat_Close,
    HiHat_Foot,     //  --> フットスプラッシュ
    HiHat_Control,  //	--> 開度（入力信号である）
    Snare,
    Snare_OpenRim,
    Snare_ClosedRim,
    //Snare_Ghost,		--> ヒット判定しない。
    Bass,
    Tom1,
    Tom1_Rim,
    Tom2,
    Tom2_Rim,
    Tom3,
    Tom3_Rim,
    RightCrash,
    //LeftCymbal_Mute,	--> （YAMAHAでは）入力信号じゃない
    //RightCymbal_Mute,	--> （YAMAHAでは）入力信号じゃない
}

public enum AutoPlayType
{
    Unknown,
    LeftCrash,
    HiHat,
    Foot,   // 左ペダル
    Snare,
    Bass,
    Tom1,
    Tom2,
    Tom3,
    RightCrash,
}

public enum InputGroupType
{
    Unknown,
    Cymbal,     // シンバルフリーモード
    LeftCymbal,
    RightCymbal,
    Ride,
    China,
    Splash,
    HiHat,
    Snare,
    Bass,
    Tom1,
    Tom2,
    Tom3,
}

public enum MuteGroupType
{
    Unknown,
    LeftCymbal,
    RightCymbal,
    HiHat,
}