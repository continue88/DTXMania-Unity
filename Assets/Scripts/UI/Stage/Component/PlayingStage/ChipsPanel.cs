using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSTFormat.v4;

public class ChipsPanel : Activity
{
    readonly PlayingStage mPlayingStage;

    public ChipsPanel(PlayingStage playingStage, GameObject gameObject)
        : base(gameObject)
    {
        mPlayingStage = playingStage;
    }

    public override void OnOpen()
    {
        base.OnOpen();

        // disable all child first.
        for (var i = 0; i < Transform.childCount; i++)
            Transform.GetChild(i).gameObject.SetActive(false);

        // get the chip slots.
        var userSettings = UserManager.Instance.LoggedOnUser;
        foreach (var value in Enum.GetValues(typeof(ChipType)))
        {
            var chipType = (ChipType)value;
            var displayName = (chipType == ChipType.BarLine || chipType == ChipType.BeatLine) ? chipType.ToString() :
                userSettings.DrumChipProperty[chipType].DisplayChipType.ToString();
            var childNode = FindChild(displayName);
            if (!childNode) continue;
            // active for this child.
            childNode.gameObject.SetActive(true);
            AddChild(new ChipSlot(childNode.gameObject, mPlayingStage, chipType));
        }
    }
}
