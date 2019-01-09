using SSTFormat.v4;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grade
{
    private float mAutoPlayModify = 1.0f;
    private float mScoreDifficulty = 5.0f;
    private Dictionary<JudgmentType, int> mJudgeToHitCount = new Dictionary<JudgmentType, int>();
    private Dictionary<JudgmentType, int> mJudgeToHitPercent = new Dictionary<JudgmentType, int>();
    private readonly Dictionary<JudgmentType, float> mJudgeAmount = new Dictionary<JudgmentType, float>()
    {
        { JudgmentType.PERFECT, 1.0f },
        { JudgmentType.GREAT,   0.5f },
        { JudgmentType.GOOD,    0.2f },
        { JudgmentType.OK,      0.0f },
        { JudgmentType.MISS,    0.0f },
    };

    public int Score { get; protected set; } = 0;
    public int Combo { get; protected set; } = 0;
    public int MaxCombo { get; protected set; } = 0;
    public float ExciteGauge { get; protected set; } = 0.75f;
    public float Achievement { get; protected set; } = 0.0f;
    public float Skill { get; protected set; } = 0.0f;
    public int TotalNotes { get; protected set; } = 0;
    public bool GradeDirty { get; protected set; } = false;

    public IReadOnlyDictionary<JudgmentType, int> JudgeToHitCount => mJudgeToHitCount;
    public IReadOnlyDictionary<JudgmentType, int> JudgeToHitPercent => mJudgeToHitPercent;

    public Grade()
    {
        foreach (JudgmentType judge in System.Enum.GetValues(typeof(JudgmentType)))
        {
            mJudgeToHitCount.Add(judge, 0);
            mJudgeToHitPercent.Add(judge, 0);
        }
        mAutoPlayModify = 1.0f;
        mScoreDifficulty = 0.5f;
    }

    public void ApplyScoreAndSetting(Score score, UserSettings settings)
    {
        TotalNotes = (null != score && null != settings) ? GetTotalNotes(score, settings) : 0;
        mScoreDifficulty = (float)(score?.Difficulty ?? 0.5);
    }

    private int GetTotalNotes(Score score, UserSettings options)
    {
        var totalNotes = 0;
        foreach (var chip in score.ChipList)
        {
            var drumChipProperty = options.DrumChipProperty[chip.ChipType];
            var judgement = true;
            var autoPlay = options.AutoPlay(drumChipProperty.AutoPlayType);
            if (autoPlay)
            {
                if (options.AutoPlayAllOn())
                    judgement = drumChipProperty.AutoPlayON_AutoJudge;
                else
                    judgement = false;
            }
            else
            {
                judgement = drumChipProperty.AutoPlayOFF_UserHitJudge;
            }


            if (judgement) totalNotes++;
        }
        return totalNotes;
    }

    public void AddHit(JudgmentType judge, int add = 1)
    {
        mJudgeToHitCount[judge] += add;
        {
            var missed = (judge == JudgmentType.OK || judge == JudgmentType.MISS);
            if (missed) Combo = 0;
            else
            {
                Combo++;
                MaxCombo = Mathf.Max(Combo, MaxCombo);
            }
        }

        {
            var basePoint = 1000000.0f / (1275.0f + 50.0f * (TotalNotes - 50));
            int comboModipy = Mathf.Min(Combo, 50);
            Score += (int)Mathf.Floor(basePoint * comboModipy * mJudgeAmount[judge]);
        }

        {
            var judgeValue = Mathf.Floor(100.0f * ((mJudgeToHitCount[JudgmentType.PERFECT] * 85.0f + mJudgeToHitCount[JudgmentType.GREAT] * 35.0f) / TotalNotes)) / 100.0f;
            var sucessRatio = 0.0f;    // 未対応
            var comboValue = Mathf.Floor(100.0f * ((MaxCombo * 5.0f / TotalNotes) + (sucessRatio * 10.0f))) / 100.0f; // 小数第3位以下切り捨て

            Achievement = (Mathf.Floor(100.0f * ((judgeValue + comboValue) * mAutoPlayModify)) / 100.0f);    // 小数第3位以下切り捨て
        }
        Skill = Mathf.Floor(100.0f * ((Achievement * mScoreDifficulty * 20.0f) / 100.0f)) / 100.0f;       // 小数第3位以下切り捨て
        GradeDirty = true;
    }

    public void AddJudgementType(JudgmentType judge)
    {
        switch (judge)
        {
            case JudgmentType.PERFECT: this.ExciteGauge += 0.025f; break;
            case JudgmentType.GREAT: this.ExciteGauge += 0.01f; break;
            case JudgmentType.GOOD: this.ExciteGauge += 0.005f; break;
            case JudgmentType.OK: this.ExciteGauge += 0f; break;
            case JudgmentType.MISS: this.ExciteGauge -= 0.08f; break;
        }
        ExciteGauge = Mathf.Max(Mathf.Min(this.ExciteGauge, 1.0f), 0.0f);
    }

    public void ReBuildIfDirty()
    {
        // reset status.
        if (!GradeDirty) return;
        GradeDirty = false;
        mJudgeToHitPercent.Clear();

        int totalHits = 0;
        var judgeToPercent = new Dictionary<JudgmentType, float>();  // 実値（0～100）
        var judgeToPercentInt = mJudgeToHitPercent; // 実値を整数にしてさらに補正した値（0～100）
        var listOfHits = new List<KeyValuePair<JudgmentType, int>>();
        var truncated = new Dictionary<JudgmentType, bool>();
        JudgmentType judgment;

        foreach (var kvp in this.mJudgeToHitCount)
            totalHits += kvp.Value;

        foreach (var kvp in this.mJudgeToHitCount)
        {
            judgeToPercent.Add(kvp.Key, (100.0f * kvp.Value) / totalHits);
            truncated.Add(kvp.Key, false);
        }

        foreach (JudgmentType j in System.Enum.GetValues(typeof(JudgmentType)))
            listOfHits.Add(new KeyValuePair<JudgmentType, int>(j, mJudgeToHitCount[j]));

        listOfHits.Sort((x, y) => (y.Value - x.Value));

        judgment = listOfHits[0].Key;
        judgeToPercentInt.Add(judgment, (int)Mathf.Floor(judgeToPercent[judgment]));
        truncated[judgment] = true;

        int judgePercentTotal = judgeToPercentInt[judgment];
        for (int i = 1; i < listOfHits.Count; i++)
        {
            judgment = listOfHits[i].Key;

            judgeToPercentInt.Add(judgment, Mathf.RoundToInt(judgeToPercent[judgment]));
            if (100 <= (judgePercentTotal + judgeToPercentInt[judgment]))
            {
                bool hasHited = false;
                for (int n = (i + 1); n < listOfHits.Count; n++)
                {
                    if (listOfHits[n].Value > 0)
                    {
                        hasHited = true;
                        break;
                    }
                }
                if (hasHited)
                {
                    judgeToPercentInt[judgment]--;
                    truncated[judgment] = true;
                }
            }

            judgePercentTotal += judgeToPercentInt[judgment];
        }

        if (100 > judgePercentTotal)
        {
            var absMap = new List<KeyValuePair<JudgmentType, float>>();

            for (int i = 1; i < listOfHits.Count; i++)
            {
                judgment = listOfHits[i].Key;
                absMap.Add(new KeyValuePair<JudgmentType, float>(judgment, Mathf.Abs(judgeToPercent[judgment] - judgeToPercentInt[judgment])));
            }

            absMap.Sort((x, y) => (int)(y.Value * 1000.0f - x.Value * 1000.0f));
            for (int i = 0; i < absMap.Count; i++)
            {
                judgment = absMap[i].Key;

                if (truncated[judgment])
                    continue;

                judgeToPercentInt[judgment]++;
                judgePercentTotal++;

                if (100 <= judgePercentTotal)
                    break;
            }
        }
    }
}
