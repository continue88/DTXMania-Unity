﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : Activity
{
    int mLastScore = -1;
    string mPreviousDigital;
    Grade mCurrentGrade;
    Animation mScoreAnim;
    List<Text> mNumberAnimInfo = new List<Text>();

    public ScoreDisplay(Grade grade, GameObject gameObject)
        : base(gameObject)
    {
        mCurrentGrade = grade;
    }

    public override void OnOpen()
    {
        base.OnOpen();

        mScoreAnim = Transform.GetComponent<Animation>();

        var numberRoot = Transform;
        for (var i = 0; i < numberRoot.childCount; i++)
        {
            var numNode = numberRoot.GetChild(i);
            mNumberAnimInfo.Add(numNode.GetComponent<Text>());
            mNumberAnimInfo[i].text = "o";
        }
        mPreviousDigital = "".PadLeft(mNumberAnimInfo.Count, 'o');
    }

    public override void Update()
    {
        base.Update();

        if (mLastScore != mCurrentGrade.Score)
        {
            UpdateScoreText(mCurrentGrade.Score);
            mLastScore = mCurrentGrade.Score;
            if (mScoreAnim.isPlaying) mScoreAnim.Stop();
            mScoreAnim.Play();
        }
    }

    void UpdateScoreText(int displayValue)
    {
        var digital = displayValue.ToString().PadLeft(mNumberAnimInfo.Count, 'o');
        for (var i = 0; i < digital.Length; i++)
        {
            if (digital[i] != mPreviousDigital[i])
                mNumberAnimInfo[mNumberAnimInfo.Count - i - 1].text = digital.Substring(i, 1);
        }
        mPreviousDigital = digital;
    }
}
