using SSTFormat.v4;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingStage : Stage
{
    int mStartDrawNumber = 0;
    ChipsPanel mChipsPanel;
    DrumPad mDrumPad;
    Dictionary<Chip, ChipPlayingState> mChipPlayingState = new Dictionary<Chip, ChipPlayingState>();

    public const float hitJudgPosY = 847f;

    public override void OnOpen()
    {
        base.OnOpen();

        mChipsPanel = AddChild(new ChipsPanel(this, FindChild("CenterPanel/ChipsPanel").gameObject));
        mDrumPad = AddChild(new DrumPad(this, FindChild("CenterPanel/DrumPad").gameObject));

        foreach (var chip in MainScript.Instance.PlayingScore.ChipList)
            mChipPlayingState.Add(chip, new ChipPlayingState(chip));
    }

    public override void Update()
    {
        base.Update();

        CheckInput();
    }

    private void CheckInput()
    {
        if (InputManager.Instance.HasCancle())
        {
            StageManager.Instance.Open<SelectionStage>();

            Close();
        }
    }

    public void ForAllChipsDrawing(ChipType chipType, System.Action<Chip, int, float, float, float> applyAction)
    {
        var score = MainScript.Instance.PlayingScore;
        if (score == null) return;

        var playingTime = GetElapsedTimeForStartPlaying();
        for (var i = mStartDrawNumber; i >= 0 && i < score.ChipList.Count; i++)
        {
            var chip = score.ChipList[i];
            if (chip.ChipType != chipType) continue;

            var drawingTime = playingTime - (float)chip.DrawTimeSec;
            var utterTime = playingTime - (float)chip.UtterTimeSec;
            var speed = MainScript.Instance.InterpSpeed;
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

    void OnChipHitted(Chip chip, JudgmentType judgeType, bool playSound, bool judge, bool hide, double time)
    {
        mChipPlayingState[chip].Hitted = true;
        if (playSound && (judgeType != JudgmentType.MISS))
        {
            if (!mChipPlayingState[chip].SoundPlayed)
            {
                PlayChipSound(chip);
                mChipPlayingState[chip].SoundPlayed = true;
            }
        }
        if (judge)
        {
            var drumChipProperty = UserManager.Instance.LoggedOnUser.DrumChipProperty[chip.ChipType];

            //if (judgeType != JudgmentType.MISS)
            //{
            //    // MISS以外（PERFECT～OK）
            //    this.mChipLight.StartViewing(対応表.DisplayTrackType);
            //    this.mDrumPad.OnHit(対応表.DisplayTrackType);
            //    this.mTrackFlash.開始する(対応表.DisplayTrackType);
            //}

            //this.mResultTextColumn.表示を開始する(対応表.DisplayTrackType, judgeType);
            //this.Results.ヒット数を加算する(judgeType);
            ////----------------
            //#endregion
        }
        if (hide)
        {
            if (judgeType == JudgmentType.MISS)
            {
                // MISSチップは最後まで表示し続ける。
            }
            else
            {
                // PERFECT～POOR チップは非表示。
                mChipPlayingState[chip].Visiable = false;
            }
        }
    }

    void PlayChipSound(Chip chip)
    {
        if (chip.ChipType == ChipType.BackGroundMovie)
        {
            // TODO: play avi movie.
        }
        else if (chip.SubChipId == 0)
        {
            var prop = UserManager.Instance.LoggedOnUser.DrumChipProperty[chip.ChipType];
            MainScript.Instance.DrumSound.PlaySound(
                chip.ChipType,
                prop.MuteBeforeUtter,
                prop.MuteGroupType,
                chip.Volume / (float)Chip.MaxVolume);
        }
        else
        {
            var prop = UserManager.Instance.LoggedOnUser.DrumChipProperty[chip.ChipType];
            WAVManager.Instance.PlaySound(
                chip.SubChipId,
                chip.ChipType,
                prop.MuteBeforeUtter,
                prop.MuteGroupType,
                chip.Volume / (float)Chip.MaxVolume);
        }
    }
}
