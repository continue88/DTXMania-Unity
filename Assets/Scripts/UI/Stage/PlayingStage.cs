using SSTFormat.v4;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingStage : Stage
{
    float mStartTime = 0;
    int mStartDrawNumber = 0;
    Grade mGrade;
    DrumPad mDrumPad;
    ChipLight mChipLight;
    ResultTextColumn mResultTextColumn;
    PlayingSpeed mPlayingSpeed;
    ChipDrawingList mChipDrawingList = new ChipDrawingList();
    Dictionary<Chip, ChipPlayingState> mChipPlayingState = new Dictionary<Chip, ChipPlayingState>();

    public const float hitJudgPosY = 600f;
    public IReadOnlyList<ChipDrawingInfo> ChipDrawingList { get { return mChipDrawingList; } }

    public override void OnOpen()
    {
        base.OnOpen();

        mGrade = new Grade();
        mGrade.ApplyScoreAndSetting(MainScript.Instance.PlayingScore, UserManager.Instance.LoggedOnUser);

        mDrumPad = AddChild(new DrumPad(this, FindChild("CenterPanel/DrumPad").gameObject));
        mChipLight = AddChild(new ChipLight(this, FindChild("ChipLight").gameObject));
        mResultTextColumn = AddChild(new ResultTextColumn(FindChild("CenterPanel/ResultTextColumn").gameObject));
        mPlayingSpeed = AddChild(new PlayingSpeed(FindChild("PlayingSpeed").gameObject));

        AddChild(new ChipsPanel(this, FindChild("CenterPanel/ChipsPanel").gameObject));
        AddChild(new PerformanceDispaly(mGrade, FindChild("LeftPanel/PerformanceDispaly").gameObject));
        AddChild(new ComboDisplay(mGrade, FindChild("RightPanel/ComboDisplay").gameObject));
        AddChild(new ScoreDisplay(mGrade, FindChild("LeftPanel/ScoreDisplay").gameObject));
        AddChild(new SongInfo(FindChild("SongInfo").gameObject));

        foreach (var chip in MainScript.Instance.PlayingScore.ChipList)
            mChipPlayingState.Add(chip, new ChipPlayingState(chip));

        InitPlayingState();
    }

    public override void Update()
    {
        base.Update();

        var playingTime = GetElapsedTimeForStartPlaying();
        mChipDrawingList.Update(MainScript.Instance.PlayingScore, playingTime, mPlayingSpeed.InterpSpeed);

        UpdateChipState();

        CheckInput();
    }

    public float GetElapsedTimeForStartPlaying()
    {
        return Time.time - mStartTime;
    }

    private void InitPlayingState()
    {
        mStartTime = Time.time;
    }

    private void UpdateChipState()
    {
        foreach (var chipDrawingInfo in mChipDrawingList)
        {
            var chip = chipDrawingInfo.Chip;
            var drawingTime = chipDrawingInfo.DrawingTime;
            var utterTime = chipDrawingInfo.UtterTime;

            var user = UserManager.Instance.LoggedOnUser;
            var drumChipProperty = user.DrumChipProperty[chip.ChipType];
            var autoPlay = user.AutoPlay(drumChipProperty.AutoPlayType);
            var hitted = mChipPlayingState[chip].Hitted;
            var notHitted = !(hitted);
            var chipMissed = (drawingTime > user.MaxHitTime(JudgmentType.OK));
            var passedHitJudgeBar = (0 <= drawingTime);
            var passedHitJudgeBarUtter = (0 <= utterTime);
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
        }
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
