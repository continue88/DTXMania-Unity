using SSTFormat.v4;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingStage : Stage
{
    int mStartDrawNumber = 0;

    public const float hitJudgPosY = 847f;

    public override void Update()
    {
        base.Update();

        if (InputManager.Instance.HasCancle())
        {
            StageManager.Instance.Open<SelectionStage>();

            Close();
        }
    }

    void UpdateInput()
    {
        var playintTime = GetElapsedTimeForStartPlaying();
        var userSettings = UserManager.Instance.LoggedOnUser;
        ForAllChipsDrawing(playintTime, (chip, index, drawingTime, utterTime, judgeDistance) =>
        {
            var drumChipProperty = userSettings.DrumChipProperty[chip.ChipType];
        });
    }

    void ForAllChipsDrawing(double playingTime, System.Action<Chip, int, double, double, double> applyAction)
    {
        var score = AppMain.Instance.PlayingScore;
        if (score == null) return;

        for (var i = mStartDrawNumber; i >= 0 && i < score.ChipList.Count; i++)
        {
            var chip = score.ChipList[i];
            var drawingTime = playingTime - chip.DrawTimeSec;
            var utterTime = playingTime - chip.UtterTimeSec;
            var speed = AppMain.Instance.InterpSpeed;
            double pixelDistance = this.GetPixleDistanceOnTime(speed, drawingTime);

            // 終了判定。
            bool aboveTopScreen = ((hitJudgPosY + pixelDistance) < -40.0);   // -40 はチップが隠れるであろう適当なマージン。
            if (aboveTopScreen)
                break;

            // 処理実行。開始判定（描画開始チップ番号の更新）もこの中で。
            applyAction(chip, i, drawingTime, utterTime, pixelDistance);
        }
    }

    private double GetPixleDistanceOnTime(double speed, double time)
    {
        const double PixelsPerMs = 0.14625 * 2.25 * 1000.0;    // これを変えると、speed あたりの速度が変わる。
        return (time * PixelsPerMs * speed);
    }

    private double GetElapsedTimeForStartPlaying()
    {
        return 0;
    }
}
