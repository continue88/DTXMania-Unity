using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSTFormat.v4;

public class ChipsPanel : Activity
{
    readonly PlayingStage mPlayingStage;
    Dictionary<DisplayChipType, ChipSlot> mChipSlots = new Dictionary<DisplayChipType, ChipSlot>();

    public ChipsPanel(PlayingStage playingStage, GameObject gameObject)
        : base(gameObject)
    {
        mPlayingStage = playingStage;
    }

    public override void OnOpen()
    {
        base.OnOpen();

        // get the chip slots.
        var userSettings = UserManager.Instance.LoggedOnUser;
        foreach (var value in Enum.GetValues(typeof(ChipType)))
        {
            var chipType = (ChipType)value;
            var displayName = (chipType == ChipType.BarLine || chipType == ChipType.BeatLine) ? chipType.ToString() :
                userSettings.DrumChipProperty[chipType].DisplayChipType.ToString();
            var childNode = FindChild(displayName);
            if (!childNode) continue;
            AddChild(new ChipSlot(childNode.gameObject, mPlayingStage, chipType));
        }
    }
}
