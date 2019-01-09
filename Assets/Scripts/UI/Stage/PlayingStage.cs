using SSTFormat.v4;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingStage : Stage
{
    float mStartTime = 0;
    int mStartDrawNumber = 0;
    Grade mGrade;
    ChipsPanel mChipsPanel;
    DrumPad mDrumPad;
    ChipLight mChipLight;
    ResultTextColumn mResultTextColumn;
    PerformanceDispaly mPerformanceDispaly;
    Dictionary<Chip, ChipPlayingState> mChipPlayingState = new Dictionary<Chip, ChipPlayingState>();

    public const float hitJudgPosY = 600f;

    public override void OnOpen()
    {
        base.OnOpen();

        mGrade = new Grade();
        mGrade.ApplyScoreAndSetting(MainScript.Instance.PlayingScore, UserManager.Instance.LoggedOnUser);

        mChipsPanel = AddChild(new ChipsPanel(this, FindChild("CenterPanel/ChipsPanel").gameObject));
        mDrumPad = AddChild(new DrumPad(this, FindChild("CenterPanel/DrumPad").gameObject));
        mChipLight = AddChild(new ChipLight(this, FindChild("ChipLight").gameObject));
        mResultTextColumn = AddChild(new ResultTextColumn(FindChild("CenterPanel/ResultTextColumn").gameObject));
        mPerformanceDispaly = AddChild(new PerformanceDispaly(mGrade, FindChild("LeftPanel/PerformanceDispaly").gameObject));
        AddChild(new SongInfo(FindChild("SongInfo").gameObject));

        foreach (var chip in MainScript.Instance.PlayingScore.ChipList)
            mChipPlayingState.Add(chip, new ChipPlayingState(chip));

        InitPlayingState();
    }

    public override void Update()
    {
        base.Update();

        UpdateChipState();

        CheckInput();
    }

    private void InitPlayingState()
    {
        mStartTime = Time.time;
    }

    private void UpdateChipState()
    {
        ForAllChipsDrawing(ChipType.Unknown, (chip, index, drawingTime, utterTime, adjustPos) =>
        {
            if (index == mStartDrawNumber && adjustPos > 0)
                mStartDrawNumber++;

            var user = UserManager.Instance.LoggedOnUser;
            var drumChipProperty = user.DrumChipProperty[chip.ChipType];
            var autoPlay = user.AutoPlay(drumChipProperty.AutoPlayType);

            bool hitted = mChipPlayingState[chip].Hitted;
            bool notHitted = !(hitted);
            bool chipMissed = (drawingTime > user.MaxHitTime(JudgmentType.OK));
            bool passedHitJudgeBar = (0 <= drawingTime);
            bool passedHitJudgeBarUtter = (0 <= utterTime);
            if (notHitted && chipMissed)
            {
                if (autoPlay && drumChipProperty.AutoPlayON_MissJudge)
                {
                    OnChipHitted(
                        chip,
                        JudgmentType.MISS,
                        drumChipProperty.AutoPlayON_AutoHitSound,
                        drumChipProperty.AutoPlayON_AutoJudge,
                        drumChipProperty.AutoPlayON_AutoHitHide,
                        utterTime);
                    return;
                }
                else if (!autoPlay && drumChipProperty.AutoPlayOFF_MissJudge)
                {
                    OnChipHitted(
                        chip,
                        JudgmentType.MISS,
                        drumChipProperty.AutoPlayOFF_UserHitSound,
                        drumChipProperty.AutoPlayOFF_UserHitJudge,
                        drumChipProperty.AutoPlayOFF_UserHitHide,
                        utterTime);
                    //this.Results.AddJudgementType(JudgmentType.MISS); // 手動演奏なら MISS はエキサイトゲージに反映。
                    return;
                }
            }
            if (passedHitJudgeBarUtter)
            {
                if ((autoPlay && drumChipProperty.AutoPlayON_AutoHitSound) ||
                    (!autoPlay && drumChipProperty.AutoPlayOFF_AutoHitSound))
                {
                    if (!(mChipPlayingState[chip].Uttered))
                    {
                        PlayChipSound(chip);
                        mChipPlayingState[chip].Uttered = true;
                    }
                }
            }
            if (notHitted && passedHitJudgeBar)
            {
                if (autoPlay && drumChipProperty.AutoPlayON_AutoHit)
                {
                    OnChipHitted(
                        chip,
                        JudgmentType.PERFECT,
                        drumChipProperty.AutoPlayON_AutoHitSound,
                        drumChipProperty.AutoPlayON_AutoJudge,
                        drumChipProperty.AutoPlayON_AutoHitHide,
                        utterTime);
                    return;
                }
                else if (!autoPlay && drumChipProperty.AutoPlayOFF_AutoHit)
                {
                    OnChipHitted(
                        chip,
                        JudgmentType.PERFECT,
                        drumChipProperty.AutoPlayOFF_AutoHitSound,
                        drumChipProperty.AutoPlayOFF_AutoHitJudge,
                        drumChipProperty.AutoPlayOFF_AutoHitHide,
                        utterTime);
                    return;
                }
            }
        });
    }

    private void CheckInput()
    {
        if (InputManager.Instance.HasCancle())
        {
            MainScript.Instance.WAVManager.Clear();

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
            if (chipType != ChipType.Unknown && chip.ChipType != chipType) continue;

            var drawingTime = playingTime - (float)chip.DrawTimeSec;
            var utterTime = playingTime - (float)chip.UtterTimeSec;
            var speed = MainScript.Instance.InterpSpeed;
            var pixelDistance = GetPixleDistanceOnTime(speed, drawingTime);

            // current chip is outof screen, stop processing.
            bool aboveTopScreen = pixelDistance < -hitJudgPosY;
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
        return Time.time - mStartTime;
    }

    void OnChipHitted(Chip chip, JudgmentType judgeType, bool playSound, bool judge, bool hide, double time)
    {
        mChipPlayingState[chip].Hitted = true;
        if (playSound && (judgeType != JudgmentType.MISS))
        {
            if (!mChipPlayingState[chip].Uttered)
            {
                PlayChipSound(chip);
                mChipPlayingState[chip].Uttered = true;
            }
        }
        if (judge)
        {
            var drumChipProperty = UserManager.Instance.LoggedOnUser.DrumChipProperty[chip.ChipType];
            if (judgeType != JudgmentType.MISS)
            {
                mDrumPad.OnHit(drumChipProperty.DisplayTrackType);
                mChipLight.OnHit(drumChipProperty.DisplayTrackType);
            }
            mResultTextColumn.OnHit(drumChipProperty.DisplayTrackType, judgeType);
            mGrade.AddHit(judgeType);
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
            MainScript.Instance.WAVManager.PlaySound(
                chip.SubChipId,
                chip.ChipType,
                prop.MuteBeforeUtter,
                prop.MuteGroupType,
                chip.Volume / (float)Chip.MaxVolume);
        }
    }
}
