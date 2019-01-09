using SSTFormat.v4;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ChipDrawingInfo
{
    public Chip Chip;
    public float DrawingTime;
    public float UtterTime;
    public float PixelDistance;
}

public class ChipDrawingList : List<ChipDrawingInfo>
{
    private int mStartDrawNumber = 0;
    public const float hitJudgPosY = 600f;

    public void Update(Score score, float playingTime, float speed)
    {
        Clear();

        for (var i = mStartDrawNumber; i >= 0 && i < score.ChipList.Count; i++)
        {
            var chip = score.ChipList[i];
            var drawingTime = playingTime - (float)chip.DrawTimeSec;
            var utterTime = playingTime - (float)chip.UtterTimeSec;
            var pixelDistance = GetPixleDistanceOnTime(speed, drawingTime);

            // current chip is outof screen, stop processing.
            var aboveTopScreen = pixelDistance < -hitJudgPosY;
            if (aboveTopScreen)
                break;

            // move forward.
            if (pixelDistance > 0 && i == mStartDrawNumber)
                mStartDrawNumber++;

            Add(new ChipDrawingInfo
            {
                Chip = chip,
                DrawingTime = drawingTime,
                UtterTime = utterTime,
                PixelDistance = pixelDistance,
            });
        }
    }

    private float GetPixleDistanceOnTime(float speed, float time)
    {
        const float PixelsPerMs = 0.14625f * 2.25f * 1000.0f;    // これを変えると、speed あたりの速度が変わる。
        return (time * PixelsPerMs * speed);
    }
}
