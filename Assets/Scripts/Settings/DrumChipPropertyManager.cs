using SSTFormat.v4;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumChipPropertyManager
{
    private PlayMode mplayMode;
    private LeftAndRightDisplayTrack mDisplaySide;
    private InputPresetType mInputPresetType;

    public Dictionary<ChipType, DrumChipProperty> ChipToProperty { get; protected set; }
    public DrumChipProperty this[ChipType chipType] => this.ChipToProperty[chipType];

    public DrumChipPropertyManager(PlayMode playMode, LeftAndRightDisplayTrack displaySide, InputPresetType presetType)
    {
        this.mplayMode = playMode;
        this.mDisplaySide = displaySide;
        this.mInputPresetType = presetType;

        this.ChipToProperty = new Dictionary<ChipType, DrumChipProperty>()
        {
            #region " チップ種別.Unknown "
            //----------------
            [ChipType.Unknown] = new DrumChipProperty()
            {
                ChipType = ChipType.Unknown,
                DrumType = DrumType.Unknown,
                DisplayTrackType = DisplayTrackType.Unknown,
                DisplayChipType = DisplayChipType.Unknown,
                DrumInputType = DrumInputType.Unknown,
                AutoPlayType = AutoPlayType.Unknown,
                InputGroupType = InputGroupType.Unknown,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = false,
                AutoPlayON_AutoHitHide = false,
                AutoPlayON_AutoJudge = false,
                AutoPlayON_MissJudge = false,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = false,
                AutoPlayOFF_UserHitHide = false,
                AutoPlayOFF_UserHitJudge = false,
                AutoPlayOFF_MissJudge = false,
            },
            //----------------
            #endregion
            #region " チップ種別.LeftCrash "
            //----------------
            [ChipType.LeftCrash] = new DrumChipProperty()
            {
                ChipType = ChipType.LeftCrash,
                DrumType = DrumType.LeftCrash,
                DisplayTrackType = DisplayTrackType.LeftCymbal,
                DisplayChipType = DisplayChipType.LeftCymbal,
                DrumInputType = DrumInputType.LeftCrash,
                AutoPlayType = AutoPlayType.LeftCrash,
                //入力グループ種別 = ...
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.LeftCymbal,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = true,
                AutoPlayON_MissJudge = true,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = true,
                AutoPlayOFF_MissJudge = true,
            },
            //----------------
            #endregion
            #region " チップ種別.Ride "
            //----------------
            [ChipType.Ride] = new DrumChipProperty()
            {
                ChipType = ChipType.Ride,
                DrumType = DrumType.Ride,
                //表示レーン種別 = ...
                //表示チップ種別 = ...
                DrumInputType = DrumInputType.Ride,
                //AutoPlay種別 = ...
                //入力グループ種別 = ...
                MuteBeforeUtter = false,
                //消音グループ種別 = ...
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = true,
                AutoPlayON_MissJudge = true,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = true,
                AutoPlayOFF_MissJudge = true,
            },
            //----------------
            #endregion
            #region " チップ種別.Ride_Cup "
            //----------------
            [ChipType.Ride_Cup] = new DrumChipProperty()
            {
                ChipType = ChipType.Ride_Cup,
                DrumType = DrumType.Ride,
                //表示レーン種別 = ...
                //表示チップ種別 = ...
                DrumInputType = DrumInputType.Ride,
                //AutoPlay種別 = ...
                //入力グループ種別 = ...
                MuteBeforeUtter = false,
                //消音グループ種別 = ...
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = true,
                AutoPlayON_MissJudge = true,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = true,
                AutoPlayOFF_MissJudge = true,
            },
            //----------------
            #endregion
            #region " チップ種別.China "
            //----------------
            [ChipType.China] = new DrumChipProperty()
            {
                ChipType = ChipType.China,
                DrumType = DrumType.China,
                //表示レーン種別 = ...
                //表示チップ種別 = ...
                DrumInputType = DrumInputType.China,
                //AutoPlay種別 = ...
                //入力グループ種別 = ...
                MuteBeforeUtter = false,
                //消音グループ種別 = ...
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = true,
                AutoPlayON_MissJudge = true,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = true,
                AutoPlayOFF_MissJudge = true,
            },
            //----------------
            #endregion
            #region " チップ種別.Splash "
            //----------------
            [ChipType.Splash] = new DrumChipProperty()
            {
                ChipType = ChipType.Splash,
                DrumType = DrumType.Splash,
                //表示レーン種別 = ...
                //表示チップ種別 = ...
                DrumInputType = DrumInputType.Splash,
                //AutoPlay種別 = ...
                //入力グループ種別 = ...
                MuteBeforeUtter = false,
                //消音グループ種別 = ...
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = true,
                AutoPlayON_MissJudge = true,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = true,
                AutoPlayOFF_MissJudge = true,
            },
            //----------------
            #endregion
            #region " チップ種別.HiHat_Open "
            //----------------
            [ChipType.HiHat_Open] = new DrumChipProperty()
            {
                ChipType = ChipType.HiHat_Open,
                DrumType = DrumType.HiHat,
                DisplayTrackType = DisplayTrackType.HiHat,
                //表示チップ種別 = ...
                DrumInputType = DrumInputType.HiHat_Open,
                AutoPlayType = AutoPlayType.HiHat,
                //入力グループ種別 = ...
                MuteBeforeUtter = true,
                MuteGroupType = MuteGroupType.HiHat,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = true,
                AutoPlayON_MissJudge = true,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = true,
                AutoPlayOFF_MissJudge = true,
            },
            //----------------
            #endregion
            #region " チップ種別.HiHat_HalfOpen "
            //----------------
            [ChipType.HiHat_HalfOpen] = new DrumChipProperty()
            {
                ChipType = ChipType.HiHat_HalfOpen,
                DrumType = DrumType.HiHat,
                DisplayTrackType = DisplayTrackType.HiHat,
                //表示チップ種別 = ...
                DrumInputType = DrumInputType.HiHat_Open,
                AutoPlayType = AutoPlayType.HiHat,
                //入力グループ種別 = ...
                MuteBeforeUtter = true,
                MuteGroupType = MuteGroupType.HiHat,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = true,
                AutoPlayON_MissJudge = true,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = true,
                AutoPlayOFF_MissJudge = true,
            },
            //----------------
            #endregion
            #region " チップ種別.HiHat_Close "
            //----------------
            [ChipType.HiHat_Close] = new DrumChipProperty()
            {
                ChipType = ChipType.HiHat_Close,
                DrumType = DrumType.HiHat,
                DisplayTrackType = DisplayTrackType.HiHat,
                DisplayChipType = DisplayChipType.HiHat,
                DrumInputType = DrumInputType.HiHat_Close,
                AutoPlayType = AutoPlayType.HiHat,
                //入力グループ種別 = ...
                MuteBeforeUtter = true,
                MuteGroupType = MuteGroupType.HiHat,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = true,
                AutoPlayON_MissJudge = true,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = true,
                AutoPlayOFF_MissJudge = true,
            },
            //----------------
            #endregion
            #region " チップ種別.HiHat_Foot "
            //----------------
            [ChipType.HiHat_Foot] = new DrumChipProperty()
            {
                ChipType = ChipType.HiHat_Foot,
                DrumType = DrumType.Foot,
                DisplayTrackType = DisplayTrackType.Foot,
                //表示チップ種別 = ...
                DrumInputType = DrumInputType.HiHat_Foot,
                AutoPlayType = AutoPlayType.Foot,
                //入力グループ種別 = ...
                MuteBeforeUtter = true,
                MuteGroupType = MuteGroupType.HiHat,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = true,
                AutoPlayON_MissJudge = false,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = true,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = false,
                AutoPlayOFF_MissJudge = false,
            },
            //----------------
            #endregion
            #region " チップ種別.Snare "
            //----------------
            [ChipType.Snare] = new DrumChipProperty()
            {
                ChipType = ChipType.Snare,
                DrumType = DrumType.Snare,
                DisplayTrackType = DisplayTrackType.Snare,
                DisplayChipType = DisplayChipType.Snare,
                DrumInputType = DrumInputType.Snare,
                AutoPlayType = AutoPlayType.Snare,
                InputGroupType = InputGroupType.Snare,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = true,
                AutoPlayON_MissJudge = true,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = true,
                AutoPlayOFF_MissJudge = true,
            },
            //----------------
            #endregion
            #region " チップ種別.Snare_OpenRim "
            //----------------
            [ChipType.Snare_OpenRim] = new DrumChipProperty()
            {
                ChipType = ChipType.Snare_OpenRim,
                DrumType = DrumType.Snare,
                DisplayTrackType = DisplayTrackType.Snare,
                //表示チップ種別 = ...
                DrumInputType = DrumInputType.Snare_OpenRim,
                AutoPlayType = AutoPlayType.Snare,
                InputGroupType = InputGroupType.Snare,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = true,
                AutoPlayON_MissJudge = true,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = true,
                AutoPlayOFF_MissJudge = true,
            },
            //----------------
            #endregion
            #region " チップ種別.Snare_ClosedRim "
            //----------------
            [ChipType.Snare_ClosedRim] = new DrumChipProperty()
            {
                ChipType = ChipType.Snare_ClosedRim,
                DrumType = DrumType.Snare,
                DisplayTrackType = DisplayTrackType.Snare,
                //表示チップ種別 = ...
                DrumInputType = DrumInputType.Snare_ClosedRim,
                AutoPlayType = AutoPlayType.Snare,
                InputGroupType = InputGroupType.Snare,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = true,
                AutoPlayON_MissJudge = true,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = true,
                AutoPlayOFF_MissJudge = true,
            },
            //----------------
            #endregion
            #region " チップ種別.Snare_Ghost "
            //----------------
            [ChipType.Snare_Ghost] = new DrumChipProperty()
            {
                ChipType = ChipType.Snare_Ghost,
                DrumType = DrumType.Snare,
                DisplayTrackType = DisplayTrackType.Snare,
                //表示チップ種別 = ...
                DrumInputType = DrumInputType.Snare,
                AutoPlayType = AutoPlayType.Snare,
                InputGroupType = InputGroupType.Snare,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = false,
                AutoPlayON_MissJudge = false,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = true,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = false,
                AutoPlayOFF_MissJudge = false,
            },
            //----------------
            #endregion
            #region " チップ種別.Bass "
            //----------------
            [ChipType.Bass] = new DrumChipProperty()
            {
                ChipType = ChipType.Bass,
                DrumType = DrumType.Bass,
                DisplayTrackType = DisplayTrackType.Bass,
                DisplayChipType = DisplayChipType.Bass,
                DrumInputType = DrumInputType.Bass,
                AutoPlayType = AutoPlayType.Bass,
                InputGroupType = InputGroupType.Bass,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = true,
                AutoPlayON_MissJudge = true,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = true,
                AutoPlayOFF_MissJudge = true,
            },
            //----------------
            #endregion
            #region " チップ種別.LeftBass "
            //----------------
            [ChipType.LeftBass] = new DrumChipProperty()
            {
                ChipType = ChipType.LeftBass,
                DrumType = DrumType.Bass,
                //表示レーン種別 = ...
                //表示チップ種別 = ...
                DrumInputType = DrumInputType.Bass,
                AutoPlayType = AutoPlayType.Bass,
                InputGroupType = InputGroupType.Bass,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = true,
                AutoPlayON_MissJudge = true,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = true,
                AutoPlayOFF_MissJudge = true,
            },
            //----------------
            #endregion
            #region " チップ種別.Tom1 "
            //----------------
            [ChipType.Tom1] = new DrumChipProperty()
            {
                ChipType = ChipType.Tom1,
                DrumType = DrumType.Tom1,
                DisplayTrackType = DisplayTrackType.Tom1,
                DisplayChipType = DisplayChipType.Tom1,
                DrumInputType = DrumInputType.Tom1,
                AutoPlayType = AutoPlayType.Tom1,
                InputGroupType = InputGroupType.Tom1,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = true,
                AutoPlayON_MissJudge = true,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = true,
                AutoPlayOFF_MissJudge = true,
            },
            //----------------
            #endregion
            #region " チップ種別.Tom1_Rim "
            //----------------
            [ChipType.Tom1_Rim] = new DrumChipProperty()
            {
                ChipType = ChipType.Tom1_Rim,
                DrumType = DrumType.Tom1,
                DisplayTrackType = DisplayTrackType.Tom1,
                //表示チップ種別 = ...
                DrumInputType = DrumInputType.Tom1_Rim,
                AutoPlayType = AutoPlayType.Tom1,
                InputGroupType = InputGroupType.Tom1,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = true,
                AutoPlayON_MissJudge = true,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = true,
                AutoPlayOFF_MissJudge = true,
            },
            //----------------
            #endregion
            #region " チップ種別.Tom2 "
            //----------------
            [ChipType.Tom2] = new DrumChipProperty()
            {
                ChipType = ChipType.Tom2,
                DrumType = DrumType.Tom2,
                DisplayTrackType = DisplayTrackType.Tom2,
                DisplayChipType = DisplayChipType.Tom2,
                DrumInputType = DrumInputType.Tom2,
                AutoPlayType = AutoPlayType.Tom2,
                InputGroupType = InputGroupType.Tom2,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = true,
                AutoPlayON_MissJudge = true,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = true,
                AutoPlayOFF_MissJudge = true,
            },
            //----------------
            #endregion
            #region " チップ種別.Tom2_Rim "
            //----------------
            [ChipType.Tom2_Rim] = new DrumChipProperty()
            {
                ChipType = ChipType.Tom2_Rim,
                DrumType = DrumType.Tom2,
                DisplayTrackType = DisplayTrackType.Tom2,
                //表示チップ種別 = ...
                DrumInputType = DrumInputType.Tom2_Rim,
                AutoPlayType = AutoPlayType.Tom2,
                InputGroupType = InputGroupType.Tom2,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = true,
                AutoPlayON_MissJudge = true,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = true,
                AutoPlayOFF_MissJudge = true,
            },
            //----------------
            #endregion
            #region " チップ種別.Tom3 "
            //----------------
            [ChipType.Tom3] = new DrumChipProperty()
            {
                ChipType = ChipType.Tom3,
                DrumType = DrumType.Tom3,
                DisplayTrackType = DisplayTrackType.Tom3,
                DisplayChipType = DisplayChipType.Tom3,
                DrumInputType = DrumInputType.Tom3,
                AutoPlayType = AutoPlayType.Tom3,
                InputGroupType = InputGroupType.Tom3,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = true,
                AutoPlayON_MissJudge = true,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = true,
                AutoPlayOFF_MissJudge = true,
            },
            //----------------
            #endregion
            #region " チップ種別.Tom3_Rim "
            //----------------
            [ChipType.Tom3_Rim] = new DrumChipProperty()
            {
                ChipType = ChipType.Tom3_Rim,
                DrumType = DrumType.Tom3,
                DisplayTrackType = DisplayTrackType.Tom3,
                //表示チップ種別 = ...
                DrumInputType = DrumInputType.Tom3_Rim,
                AutoPlayType = AutoPlayType.Tom3,
                InputGroupType = InputGroupType.Tom3,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = true,
                AutoPlayON_MissJudge = true,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = true,
                AutoPlayOFF_MissJudge = true,
            },
            //----------------
            #endregion
            #region " チップ種別.RightCrash "
            //----------------
            [ChipType.RightCrash] = new DrumChipProperty()
            {
                ChipType = ChipType.RightCrash,
                DrumType = DrumType.RightCrash,
                DisplayTrackType = DisplayTrackType.RightCymbal,
                DisplayChipType = DisplayChipType.RightCymbal,
                DrumInputType = DrumInputType.RightCrash,
                AutoPlayType = AutoPlayType.RightCrash,
                //入力グループ種別 = ...
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.RightCymbal,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = true,
                AutoPlayON_MissJudge = true,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = true,
                AutoPlayOFF_UserHitHide = true,
                AutoPlayOFF_UserHitJudge = true,
                AutoPlayOFF_MissJudge = true,
            },
            //----------------
            #endregion
            #region " チップ種別.BPM "
            //----------------
            [ChipType.BPM] = new DrumChipProperty()
            {
                ChipType = ChipType.BPM,
                DrumType = DrumType.BPM,
                DisplayTrackType = DisplayTrackType.Unknown,
                DisplayChipType = DisplayChipType.Unknown,
                DrumInputType = DrumInputType.Unknown,
                AutoPlayType = AutoPlayType.Unknown,
                InputGroupType = InputGroupType.Unknown,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = false,
                AutoPlayON_AutoHitHide = false,
                AutoPlayON_AutoJudge = false,
                AutoPlayON_MissJudge = false,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = false,
                AutoPlayOFF_UserHitHide = false,
                AutoPlayOFF_UserHitJudge = false,
                AutoPlayOFF_MissJudge = false,
            },
            //----------------
            #endregion
            #region " チップ種別.小節線 "
            //----------------
            [ChipType.BarLine] = new DrumChipProperty()
            {
                ChipType = ChipType.BarLine,
                DrumType = DrumType.Unknown,
                DisplayTrackType = DisplayTrackType.Unknown,
                DisplayChipType = DisplayChipType.Unknown,
                DrumInputType = DrumInputType.Unknown,
                AutoPlayType = AutoPlayType.Unknown,
                InputGroupType = InputGroupType.Unknown,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = false,
                AutoPlayON_AutoHitHide = false,
                AutoPlayON_AutoJudge = false,
                AutoPlayON_MissJudge = false,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = false,
                AutoPlayOFF_UserHitHide = false,
                AutoPlayOFF_UserHitJudge = false,
                AutoPlayOFF_MissJudge = false,
            },
            //----------------
            #endregion
            #region " チップ種別.拍線 "
            //----------------
            [ChipType.BeatLine] = new DrumChipProperty()
            {
                ChipType = ChipType.BeatLine,
                DrumType = DrumType.Unknown,
                DisplayTrackType = DisplayTrackType.Unknown,
                DisplayChipType = DisplayChipType.Unknown,
                DrumInputType = DrumInputType.Unknown,
                AutoPlayType = AutoPlayType.Unknown,
                InputGroupType = InputGroupType.Unknown,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = false,
                AutoPlayON_AutoHitHide = false,
                AutoPlayON_AutoJudge = false,
                AutoPlayON_MissJudge = false,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = false,
                AutoPlayOFF_UserHitHide = false,
                AutoPlayOFF_UserHitJudge = false,
                AutoPlayOFF_MissJudge = false,
            },
            //----------------
            #endregion
            #region " チップ種別.BGV "
            //----------------
            [ChipType.BackGroundMovie] = new DrumChipProperty()
            {
                ChipType = ChipType.BackGroundMovie,
                DrumType = DrumType.BGV,
                DisplayTrackType = DisplayTrackType.Unknown,
                DisplayChipType = DisplayChipType.Unknown,
                DrumInputType = DrumInputType.Unknown,
                AutoPlayType = AutoPlayType.Unknown,
                InputGroupType = InputGroupType.Unknown,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = false,
                AutoPlayON_MissJudge = false,
                AutoPlayOFF_AutoHitSound = true,
                AutoPlayOFF_AutoHitHide = true,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = false,
                AutoPlayOFF_UserHitHide = false,
                AutoPlayOFF_UserHitJudge = false,
                AutoPlayOFF_MissJudge = false,
            },
            //----------------
            #endregion
            #region " チップ種別.小節メモ "
            //----------------
            [ChipType.BarNote] = new DrumChipProperty()
            {
                ChipType = ChipType.BarNote,
                DrumType = DrumType.Unknown,
                DisplayTrackType = DisplayTrackType.Unknown,
                DisplayChipType = DisplayChipType.Unknown,
                DrumInputType = DrumInputType.Unknown,
                AutoPlayType = AutoPlayType.Unknown,
                InputGroupType = InputGroupType.Unknown,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = false,
                AutoPlayON_AutoHitHide = false,
                AutoPlayON_AutoJudge = false,
                AutoPlayON_MissJudge = false,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = false,
                AutoPlayOFF_UserHitHide = false,
                AutoPlayOFF_UserHitJudge = false,
                AutoPlayOFF_MissJudge = false,
            },
            //----------------
            #endregion
            #region " チップ種別.LeftCymbal_Mute "
            //----------------
            [ChipType.LeftCymbal_Mute] = new DrumChipProperty()
            {
                ChipType = ChipType.LeftCymbal_Mute,
                DrumType = DrumType.LeftCrash,
                //表示レーン種別 = ...
                //表示チップ種別 = ...
                DrumInputType = DrumInputType.Unknown,
                AutoPlayType = AutoPlayType.LeftCrash,
                InputGroupType = InputGroupType.Unknown,
                MuteBeforeUtter = true,
                MuteGroupType = MuteGroupType.LeftCymbal,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = false,
                AutoPlayON_MissJudge = false,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = true,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = false,
                AutoPlayOFF_UserHitHide = false,
                AutoPlayOFF_UserHitJudge = false,
                AutoPlayOFF_MissJudge = false,
            },
            //----------------
            #endregion
            #region " チップ種別.RightCymbal_Mute "
            //----------------
            [ChipType.RightCymbal_Mute] = new DrumChipProperty()
            {
                ChipType = ChipType.RightCymbal_Mute,
                DrumType = DrumType.RightCrash,
                //表示レーン種別 = ...
                //表示チップ種別 = ...
                DrumInputType = DrumInputType.Unknown,
                AutoPlayType = AutoPlayType.RightCrash,
                InputGroupType = InputGroupType.Unknown,
                MuteBeforeUtter = true,
                MuteGroupType = MuteGroupType.RightCymbal,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = false,
                AutoPlayON_MissJudge = false,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = true,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = false,
                AutoPlayOFF_UserHitHide = false,
                AutoPlayOFF_UserHitJudge = false,
                AutoPlayOFF_MissJudge = false,
            },
            //----------------
            #endregion
            #region " チップ種別.小節の先頭 "
            //----------------
            [ChipType.BarBegin] = new DrumChipProperty()
            {
                ChipType = ChipType.BarBegin,
                DrumType = DrumType.Unknown,
                DisplayTrackType = DisplayTrackType.Unknown,
                DisplayChipType = DisplayChipType.Unknown,
                DrumInputType = DrumInputType.Unknown,
                AutoPlayType = AutoPlayType.Unknown,
                InputGroupType = InputGroupType.Unknown,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = false,
                AutoPlayON_AutoHitHide = false,
                AutoPlayON_AutoJudge = false,
                AutoPlayON_MissJudge = false,
                AutoPlayOFF_AutoHitSound = false,
                AutoPlayOFF_AutoHitHide = false,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = false,
                AutoPlayOFF_UserHitHide = false,
                AutoPlayOFF_UserHitJudge = false,
                AutoPlayOFF_MissJudge = false,
            },
            //----------------
            #endregion
            #region " チップ種別.BGM "
            //----------------
            [ChipType.BGM] = new DrumChipProperty()
            {
                ChipType = ChipType.BGM,
                DrumType = DrumType.BGM,
                DisplayTrackType = DisplayTrackType.Unknown,
                DisplayChipType = DisplayChipType.Unknown,
                DrumInputType = DrumInputType.Unknown,
                AutoPlayType = AutoPlayType.Unknown,
                InputGroupType = InputGroupType.Unknown,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = false,
                AutoPlayON_MissJudge = false,
                AutoPlayOFF_AutoHitSound = true,
                AutoPlayOFF_AutoHitHide = true,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = false,
                AutoPlayOFF_UserHitHide = false,
                AutoPlayOFF_UserHitJudge = false,
                AutoPlayOFF_MissJudge = false,
            },
            //----------------
            #endregion
            #region " チップ種別.SE1 "
            //----------------
            [ChipType.SE1] = new DrumChipProperty()
            {
                ChipType = ChipType.SE1,
                DrumType = DrumType.Unknown,
                DisplayTrackType = DisplayTrackType.Unknown,
                DisplayChipType = DisplayChipType.Unknown,
                DrumInputType = DrumInputType.Unknown,
                AutoPlayType = AutoPlayType.Unknown,
                InputGroupType = InputGroupType.Unknown,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = false,
                AutoPlayON_MissJudge = false,
                AutoPlayOFF_AutoHitSound = true,
                AutoPlayOFF_AutoHitHide = true,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = false,
                AutoPlayOFF_UserHitHide = false,
                AutoPlayOFF_UserHitJudge = false,
                AutoPlayOFF_MissJudge = false,
            },
            //----------------
            #endregion
            #region " チップ種別.SE2 "
            //----------------
            [ChipType.SE2] = new DrumChipProperty()
            {
                ChipType = ChipType.SE2,
                DrumType = DrumType.Unknown,
                DisplayTrackType = DisplayTrackType.Unknown,
                DisplayChipType = DisplayChipType.Unknown,
                DrumInputType = DrumInputType.Unknown,
                AutoPlayType = AutoPlayType.Unknown,
                InputGroupType = InputGroupType.Unknown,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = false,
                AutoPlayON_MissJudge = false,
                AutoPlayOFF_AutoHitSound = true,
                AutoPlayOFF_AutoHitHide = true,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = false,
                AutoPlayOFF_UserHitHide = false,
                AutoPlayOFF_UserHitJudge = false,
                AutoPlayOFF_MissJudge = false,
            },
            //----------------
            #endregion
            #region " チップ種別.SE3 "
            //----------------
            [ChipType.SE3] = new DrumChipProperty()
            {
                ChipType = ChipType.SE3,
                DrumType = DrumType.Unknown,
                DisplayTrackType = DisplayTrackType.Unknown,
                DisplayChipType = DisplayChipType.Unknown,
                DrumInputType = DrumInputType.Unknown,
                AutoPlayType = AutoPlayType.Unknown,
                InputGroupType = InputGroupType.Unknown,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = false,
                AutoPlayON_MissJudge = false,
                AutoPlayOFF_AutoHitSound = true,
                AutoPlayOFF_AutoHitHide = true,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = false,
                AutoPlayOFF_UserHitHide = false,
                AutoPlayOFF_UserHitJudge = false,
                AutoPlayOFF_MissJudge = false,
            },
            //----------------
            #endregion
            #region " チップ種別.SE4 "
            //----------------
            [ChipType.SE4] = new DrumChipProperty()
            {
                ChipType = ChipType.SE4,
                DrumType = DrumType.Unknown,
                DisplayTrackType = DisplayTrackType.Unknown,
                DisplayChipType = DisplayChipType.Unknown,
                DrumInputType = DrumInputType.Unknown,
                AutoPlayType = AutoPlayType.Unknown,
                InputGroupType = InputGroupType.Unknown,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = false,
                AutoPlayON_MissJudge = false,
                AutoPlayOFF_AutoHitSound = true,
                AutoPlayOFF_AutoHitHide = true,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = false,
                AutoPlayOFF_UserHitHide = false,
                AutoPlayOFF_UserHitJudge = false,
                AutoPlayOFF_MissJudge = false,
            },
            //----------------
            #endregion
            #region " チップ種別.SE5 "
            //----------------
            [ChipType.SE5] = new DrumChipProperty()
            {
                ChipType = ChipType.SE5,
                DrumType = DrumType.Unknown,
                DisplayTrackType = DisplayTrackType.Unknown,
                DisplayChipType = DisplayChipType.Unknown,
                DrumInputType = DrumInputType.Unknown,
                AutoPlayType = AutoPlayType.Unknown,
                InputGroupType = InputGroupType.Unknown,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = false,
                AutoPlayON_MissJudge = false,
                AutoPlayOFF_AutoHitSound = true,
                AutoPlayOFF_AutoHitHide = true,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = false,
                AutoPlayOFF_UserHitHide = false,
                AutoPlayOFF_UserHitJudge = false,
                AutoPlayOFF_MissJudge = false,
            },
            //----------------
            #endregion
            #region " チップ種別.GuitarAuto "
            //----------------
            [ChipType.GuitarAuto] = new DrumChipProperty()
            {
                ChipType = ChipType.GuitarAuto,
                DrumType = DrumType.Unknown,
                DisplayTrackType = DisplayTrackType.Unknown,
                DisplayChipType = DisplayChipType.Unknown,
                DrumInputType = DrumInputType.Unknown,
                AutoPlayType = AutoPlayType.Unknown,
                InputGroupType = InputGroupType.Unknown,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = false,
                AutoPlayON_MissJudge = false,
                AutoPlayOFF_AutoHitSound = true,
                AutoPlayOFF_AutoHitHide = true,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = false,
                AutoPlayOFF_UserHitHide = false,
                AutoPlayOFF_UserHitJudge = false,
                AutoPlayOFF_MissJudge = false,
            },
            //----------------
            #endregion
            #region " チップ種別.BassAuto "
            //----------------
            [ChipType.BassAuto] = new DrumChipProperty()
            {
                ChipType = ChipType.BassAuto,
                DrumType = DrumType.Unknown,
                DisplayTrackType = DisplayTrackType.Unknown,
                DisplayChipType = DisplayChipType.Unknown,
                DrumInputType = DrumInputType.Unknown,
                AutoPlayType = AutoPlayType.Unknown,
                InputGroupType = InputGroupType.Unknown,
                MuteBeforeUtter = false,
                MuteGroupType = MuteGroupType.Unknown,
                AutoPlayON_AutoHitSound = true,
                AutoPlayON_AutoHitHide = true,
                AutoPlayON_AutoJudge = false,
                AutoPlayON_MissJudge = false,
                AutoPlayOFF_AutoHitSound = true,
                AutoPlayOFF_AutoHitHide = true,
                AutoPlayOFF_AutoHitJudge = false,
                AutoPlayOFF_UserHitSound = false,
                AutoPlayOFF_UserHitHide = false,
                AutoPlayOFF_UserHitJudge = false,
                AutoPlayOFF_MissJudge = false,
            },
            //----------------
            #endregion
        };

        this.反映する(playMode);
        this.反映する(displaySide);
        this.反映する(presetType);
    }

    /// <summary>
    ///     演奏モードに依存するメンバに対して一括設定を行う。
    ///     依存しないメンバには何もしない。
    /// </summary>
    public void 反映する(PlayMode mode)
    {
        this.mplayMode = mode;

        foreach (var kvp in this.ChipToProperty)
        {
            switch (kvp.Key)
            {
                case ChipType.Ride:
                    kvp.Value.DisplayChipType = (this.mplayMode == PlayMode.EXPERT) ?
                        ((this.mDisplaySide.RideLeft) ? DisplayChipType.LeftRide : DisplayChipType.RightRide) :
                        ((this.mDisplaySide.RideLeft) ? DisplayChipType.LeftCymbal : DisplayChipType.RightCymbal);
                    break;

                case ChipType.Ride_Cup:
                    kvp.Value.DisplayChipType = (this.mplayMode == PlayMode.EXPERT) ?
                        ((this.mDisplaySide.RideLeft) ? DisplayChipType.LeftRide_Cup : DisplayChipType.RightRide_Cup) :
                        ((this.mDisplaySide.RideLeft) ? DisplayChipType.LeftCymbal : DisplayChipType.RightCymbal);
                    break;

                case ChipType.China:
                    kvp.Value.DisplayChipType = (this.mplayMode == PlayMode.EXPERT) ?
                        ((this.mDisplaySide.ChinaLeft) ? DisplayChipType.LeftChina : DisplayChipType.RightChina) :
                        ((this.mDisplaySide.ChinaLeft) ? DisplayChipType.LeftCymbal : DisplayChipType.RightCymbal);
                    break;

                case ChipType.Splash:
                    kvp.Value.DisplayChipType = (this.mplayMode == PlayMode.EXPERT) ?
                        ((this.mDisplaySide.SplashLeft) ? DisplayChipType.LeftSplash : DisplayChipType.RightSplash) :
                        ((this.mDisplaySide.SplashLeft) ? DisplayChipType.LeftCymbal : DisplayChipType.RightCymbal);
                    break;

                case ChipType.HiHat_Open:
                    kvp.Value.DisplayChipType = (this.mplayMode == PlayMode.EXPERT) ? DisplayChipType.HiHat_Open : DisplayChipType.HiHat;
                    break;

                case ChipType.HiHat_HalfOpen:
                    kvp.Value.DisplayChipType = (this.mplayMode == PlayMode.EXPERT) ? DisplayChipType.HiHat_HalfOpen : DisplayChipType.HiHat;
                    break;

                case ChipType.HiHat_Foot:
                    kvp.Value.DisplayChipType = (this.mplayMode == PlayMode.EXPERT) ? DisplayChipType.Foot : DisplayChipType.LeftPedal;
                    break;

                case ChipType.Snare_OpenRim:
                    kvp.Value.DisplayChipType = (this.mplayMode == PlayMode.EXPERT) ? DisplayChipType.Snare_OpenRim : DisplayChipType.Snare;
                    break;

                case ChipType.Snare_ClosedRim:
                    kvp.Value.DisplayChipType = (this.mplayMode == PlayMode.EXPERT) ? DisplayChipType.Snare_ClosedRim : DisplayChipType.Snare;
                    break;

                case ChipType.Snare_Ghost:
                    kvp.Value.DisplayChipType = (this.mplayMode == PlayMode.EXPERT) ? DisplayChipType.Snare_Ghost : DisplayChipType.Unknown;
                    break;

                case ChipType.LeftBass:
                    kvp.Value.DisplayChipType = (this.mplayMode == PlayMode.EXPERT) ? DisplayChipType.Bass : DisplayChipType.LeftBass;
                    kvp.Value.DisplayTrackType = (this.mplayMode == PlayMode.EXPERT) ? DisplayTrackType.Bass : DisplayTrackType.Foot;
                    break;

                case ChipType.Tom1_Rim:
                    kvp.Value.DisplayChipType = (this.mplayMode == PlayMode.EXPERT) ? DisplayChipType.Tom1_Rim : DisplayChipType.Tom1;
                    break;

                case ChipType.Tom2_Rim:
                    kvp.Value.DisplayChipType = (this.mplayMode == PlayMode.EXPERT) ? DisplayChipType.Tom2_Rim : DisplayChipType.Tom2;
                    break;

                case ChipType.Tom3_Rim:
                    kvp.Value.DisplayChipType = (this.mplayMode == PlayMode.EXPERT) ? DisplayChipType.Tom3_Rim : DisplayChipType.Tom3;
                    break;

                case ChipType.LeftCymbal_Mute:
                    kvp.Value.DisplayChipType = (this.mplayMode == PlayMode.EXPERT) ? DisplayChipType.LeftCymbal_Mute : DisplayChipType.Unknown;
                    kvp.Value.DisplayTrackType = (this.mplayMode == PlayMode.EXPERT) ? DisplayTrackType.LeftCymbal : DisplayTrackType.Unknown;
                    break;

                case ChipType.RightCymbal_Mute:
                    kvp.Value.DisplayChipType = (this.mplayMode == PlayMode.EXPERT) ? DisplayChipType.RightCymbal_Mute : DisplayChipType.Unknown;
                    kvp.Value.DisplayTrackType = (this.mplayMode == PlayMode.EXPERT) ? DisplayTrackType.RightCymbal : DisplayTrackType.Unknown;
                    break;
            }
        }
    }

    /// <summary>
    ///     表示レーンの左右に依存するメンバに対して一括設定を行う。
    ///     依存しないメンバには何もしない。
    /// </summary>
    public void 反映する(LeftAndRightDisplayTrack position)
    {
        this.mDisplaySide = position;

        foreach (var kvp in this.ChipToProperty)
        {
            switch (kvp.Key)
            {
                case ChipType.Ride:
                    kvp.Value.DisplayTrackType = (this.mDisplaySide.RideLeft) ? DisplayTrackType.LeftCymbal : DisplayTrackType.RightCymbal;
                    kvp.Value.AutoPlayType = (this.mDisplaySide.RideLeft) ? AutoPlayType.LeftCrash : AutoPlayType.RightCrash;
                    kvp.Value.MuteGroupType = (this.mDisplaySide.RideLeft) ? MuteGroupType.LeftCymbal : MuteGroupType.RightCymbal;
                    kvp.Value.DisplayChipType = (this.mplayMode == PlayMode.EXPERT) ?
                        ((this.mDisplaySide.RideLeft) ? DisplayChipType.LeftRide : DisplayChipType.RightRide) :
                        ((this.mDisplaySide.RideLeft) ? DisplayChipType.LeftCymbal : DisplayChipType.RightCymbal);
                    break;

                case ChipType.Ride_Cup:
                    kvp.Value.DisplayTrackType = (this.mDisplaySide.RideLeft) ? DisplayTrackType.LeftCymbal : DisplayTrackType.RightCymbal;
                    kvp.Value.AutoPlayType = (this.mDisplaySide.RideLeft) ? AutoPlayType.LeftCrash : AutoPlayType.RightCrash;
                    kvp.Value.MuteGroupType = (this.mDisplaySide.RideLeft) ? MuteGroupType.LeftCymbal : MuteGroupType.RightCymbal;
                    kvp.Value.DisplayChipType = (this.mplayMode == PlayMode.EXPERT) ?
                        ((this.mDisplaySide.RideLeft) ? DisplayChipType.LeftRide_Cup : DisplayChipType.RightRide_Cup) :
                        ((this.mDisplaySide.RideLeft) ? DisplayChipType.LeftCymbal : DisplayChipType.RightCymbal);
                    break;

                case ChipType.China:
                    kvp.Value.DisplayTrackType = (this.mDisplaySide.ChinaLeft) ? DisplayTrackType.LeftCymbal : DisplayTrackType.RightCymbal;
                    kvp.Value.AutoPlayType = (this.mDisplaySide.ChinaLeft) ? AutoPlayType.LeftCrash : AutoPlayType.RightCrash;
                    kvp.Value.MuteGroupType = (this.mDisplaySide.ChinaLeft) ? MuteGroupType.LeftCymbal : MuteGroupType.RightCymbal;
                    kvp.Value.DisplayChipType = (this.mplayMode == PlayMode.EXPERT) ?
                        ((this.mDisplaySide.ChinaLeft) ? DisplayChipType.LeftChina : DisplayChipType.RightChina) :
                        ((this.mDisplaySide.ChinaLeft) ? DisplayChipType.LeftCymbal : DisplayChipType.RightCymbal);
                    break;

                case ChipType.Splash:
                    kvp.Value.DisplayTrackType = (this.mDisplaySide.SplashLeft) ? DisplayTrackType.LeftCymbal : DisplayTrackType.RightCymbal;
                    kvp.Value.AutoPlayType = (this.mDisplaySide.SplashLeft) ? AutoPlayType.LeftCrash : AutoPlayType.RightCrash;
                    kvp.Value.MuteGroupType = (this.mDisplaySide.SplashLeft) ? MuteGroupType.LeftCymbal : MuteGroupType.RightCymbal;
                    kvp.Value.DisplayChipType = (this.mplayMode == PlayMode.EXPERT) ?
                        ((this.mDisplaySide.SplashLeft) ? DisplayChipType.LeftSplash : DisplayChipType.RightSplash) :
                        ((this.mDisplaySide.SplashLeft) ? DisplayChipType.LeftCymbal : DisplayChipType.RightCymbal);
                    break;
            }
        }
    }

    /// <summary>
    ///     指定されたプリセットに依存する入力グループ種別を一括設定する。
    ///     依存しないメンバには何もしない。
    /// </summary>
    public void 反映する(InputPresetType preset)
    {
        this.mInputPresetType = preset;

        foreach (var kvp in this.ChipToProperty)
        {
            switch (this.mInputPresetType)
            {
                case InputPresetType.CymbalFree:

                    switch (kvp.Key)
                    {
                        case ChipType.LeftCrash:
                        case ChipType.Ride:
                        case ChipType.Ride_Cup:
                        case ChipType.China:
                        case ChipType.Splash:
                        case ChipType.HiHat_Open:
                        case ChipType.HiHat_HalfOpen:
                        case ChipType.HiHat_Close:
                        case ChipType.HiHat_Foot:
                        case ChipType.RightCrash:
                            kvp.Value.InputGroupType = InputGroupType.Cymbal;
                            break;
                    }
                    break;

                case InputPresetType.Basic:

                    switch (kvp.Key)
                    {
                        case ChipType.LeftCrash:
                            kvp.Value.InputGroupType = InputGroupType.LeftCymbal;
                            break;

                        case ChipType.Ride:
                        case ChipType.Ride_Cup:
                            kvp.Value.InputGroupType = InputGroupType.Ride;
                            break;

                        case ChipType.China:
                            kvp.Value.InputGroupType = InputGroupType.China;
                            break;

                        case ChipType.Splash:
                            kvp.Value.InputGroupType = InputGroupType.Splash;
                            break;

                        case ChipType.HiHat_Open:
                        case ChipType.HiHat_HalfOpen:
                        case ChipType.HiHat_Close:
                        case ChipType.HiHat_Foot:
                            kvp.Value.InputGroupType = InputGroupType.HiHat;
                            break;

                        case ChipType.RightCrash:
                            kvp.Value.InputGroupType = InputGroupType.RightCymbal;
                            break;
                    }
                    break;

                default:
                    throw new System.Exception($"未知の入力グループプリセット種別です。[{this.mInputPresetType.ToString()}]");
            }
        }
    }
}
