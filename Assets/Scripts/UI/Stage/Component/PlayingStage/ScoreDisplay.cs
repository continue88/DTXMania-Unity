using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : Activity
{
    int mLastScore = -1;
    string mPreviousDigital;
    Grade mCurrentGrade;
    List<NumberInfo> mNumberAnimInfo = new List<NumberInfo>();

    class NumberInfo
    {
        public Text Text;
        public Animation Anim;
    }

    public ScoreDisplay(Grade grade, GameObject gameObject)
        : base(gameObject)
    {
        mCurrentGrade = grade;
    }

    public override void OnOpen()
    {
        base.OnOpen();

        var numberRoot = Transform;
        for (var i = 0; i < numberRoot.childCount; i++)
        {
            var numNode = numberRoot.GetChild(i);
            mNumberAnimInfo.Add(new NumberInfo
            {
                Text = numNode.GetComponent<Text>(),
                Anim = numNode.GetComponent<Animation>(),
            });
            mNumberAnimInfo[i].Text.text = "o";
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
        }
    }

    void UpdateScoreText(int displayValue)
    {
        var digital = displayValue.ToString().PadLeft(mNumberAnimInfo.Count, 'o');
        for (var i = 0; i < digital.Length; i++)
        {
            if (digital[i] != mPreviousDigital[i])
            {
                mNumberAnimInfo[mNumberAnimInfo.Count - i - 1].Anim.Play();
                mNumberAnimInfo[mNumberAnimInfo.Count - i - 1].Text.text = digital.Substring(i, 1);
            }
        }
        mPreviousDigital = digital;
    }
}
