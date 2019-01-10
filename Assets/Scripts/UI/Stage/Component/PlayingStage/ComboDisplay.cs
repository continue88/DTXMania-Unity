using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboDisplay : Activity
{
    int mPreviousCombo = 0;
    string mPreviousDigital = "    ";
    Grade mCurrentGrade;
    Animation mHundredAnim;
    List<NumberInfo> mNumberAnimInfo = new List<NumberInfo>();

    class NumberInfo
    {
        public Text Text;
        public Animation Anim;
    }

    public ComboDisplay(Grade grade, GameObject gameObject)
        : base(gameObject)
    {
        mCurrentGrade = grade;
    }

    public override void OnOpen()
    {
        base.OnOpen();

        mHundredAnim = GetComponent<Animation>("Root");
        var numberRoot = FindChild("Root/Number");
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
    }

    public override void Update()
    {
        base.Update();

        if (mPreviousCombo != mCurrentGrade.Combo)
        {
            UpdateComboText(mCurrentGrade.Combo);
            mPreviousCombo = mCurrentGrade.Combo;
        }
    }

    void UpdateComboText(int combo)
    {
        var displayValue = Mathf.Min(9999, combo);
        if (displayValue < 0) return;

        var digital = displayValue.ToString().PadLeft(4, 'o');

        // play the root anim.
        if ((combo / 100) > (mPreviousCombo / 100))
        {
            if (mHundredAnim.isPlaying) mHundredAnim.Stop();
            mHundredAnim.Play("Combo100");
        }
        else if ((combo / 10) > (mPreviousCombo / 10))
            mHundredAnim.Play("Combo10");

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
