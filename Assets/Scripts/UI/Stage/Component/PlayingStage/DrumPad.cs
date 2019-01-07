using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumPad : Activity
{
    PlayingStage mPlayingStage;

    public DrumPad(PlayingStage playingStage, GameObject gameObject)
        : base(gameObject)
    {

    }

    public override void OnOpen()
    {
        base.OnOpen();
    }

    public override void Update()
    {
        base.Update();

        UpdateDrumPad();
    }

    void UpdateDrumPad()
    {
    }
}
