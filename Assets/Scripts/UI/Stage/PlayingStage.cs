using SSTFormat.v4;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingStage : Stage
{
    int mStartDrawNumber = 0;
    ChipsPanel mChipsPanel;
    DrumPad mDrumPad;

    public const float hitJudgPosY = 847f;

    public override void OnOpen()
    {
        base.OnOpen();

        mChipsPanel = AddChild(new ChipsPanel(this, FindChild("CenterPanel/ChipsPanel").gameObject));
        mDrumPad = AddChild(new DrumPad(this, FindChild("CenterPanel/DrumPad").gameObject));
    }

    public override void Update()
    {
        base.Update();
        
        if (InputManager.Instance.HasCancle())
        {
            StageManager.Instance.Open<SelectionStage>();

            Close();
        }
    }

    public void ForAllChipsDrawing(ChipType chipType, System.Action<Chip, int, float, float, float> applyAction)
    {
        var score = AppMain.Instance.PlayingScore;
        if (score == null) return;

        var playingTime = GetElapsedTimeForStartPlaying();
        for (var i = mStartDrawNumber; i >= 0 && i < score.ChipList.Count; i++)
        {
            var chip = score.ChipList[i];
            if (chip.ChipType != chipType) continue;

            var drawingTime = playingTime - (float)chip.DrawTimeSec;
            var utterTime = playingTime - (float)chip.UtterTimeSec;
            var speed = AppMain.Instance.InterpSpeed;
            var pixelDistance = GetPixleDistanceOnTime(speed, drawingTime);

            // current chip is outof screen, stop processing.
            bool aboveTopScreen = ((hitJudgPosY + pixelDistance) < -40.0);
            if (aboveTopScreen)
                break;

            // apply action to the drawing chips.
            applyAction(chip, i, drawingTime, utterTime, pixelDistance);
        }
    }

    private float GetPixleDistanceOnTime(float speed, float time)
    {
        const float PixelsPerMs = 0.14625f * 2.25f * 1000.0f;    // これを変えると、speed あたりの速度が変わる。
        return (time * PixelsPerMs * speed);
    }

    public float GetElapsedTimeForStartPlaying()
    {
        return 0;
    }
}
