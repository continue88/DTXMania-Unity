using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformanceDispaly : Activity
{
    Grade mCurrentGrade;
    Dictionary<JudgmentType, TypeNodeInfo> mJudgmentNode = new Dictionary<JudgmentType, TypeNodeInfo>();
    TypeNodeInfo mMaxComboNode;

    class TypeNodeInfo
    {
        public Text HitCount;
        public Text Percent;
    }

    public PerformanceDispaly(Grade grade, GameObject gameObject)
        : base(gameObject)
    {
        mCurrentGrade = grade;
    }

    public override void OnOpen()
    {
        base.OnOpen();

        foreach (JudgmentType type in System.Enum.GetValues(typeof(JudgmentType)))
        {
            var nodeName = type.ToString();
            var typeNode = Transform.Find(nodeName);
            if (!typeNode) continue;
            mJudgmentNode[type] = new TypeNodeInfo
            {
                HitCount = GetComponent<Text>(nodeName + "/HitCount"),
                Percent = GetComponent<Text>(nodeName + "/Percent"),
            };
        }
        mMaxComboNode = new TypeNodeInfo
        {
            HitCount = GetComponent<Text>("MAXCOMBO/HitCount"),
            Percent = GetComponent<Text>("MAXCOMBO/Percent"),
        };
    }

    public override void Update()
    {
        base.Update();

        if (mCurrentGrade.GradeDirty)
        {
            mCurrentGrade.ReBuildIfDirty();
            UpdateText(mCurrentGrade);
        }
    }

    void UpdateText(Grade grade)
    {
        var hitCount = grade.JudgeToHitCount;
        var hitPercent = grade.JudgeToHitPercent;
        var maxCombo = grade.MaxCombo;
        var total = 0;
        foreach (var typeNode in mJudgmentNode)
        {
            var hitNumber = grade.JudgeToHitCount[typeNode.Key];
            total += hitNumber;
            DrawTypeNode(typeNode.Value,
                hitNumber, 
                grade.JudgeToHitPercent[typeNode.Key]);
        }
        DrawTypeNode(mMaxComboNode, maxCombo, Mathf.RoundToInt(100 * maxCombo / total));
    }

    void DrawTypeNode(TypeNodeInfo typeNode, int hitNumber, int hitPercentage)
    { 
        typeNode.HitCount.text = BuildDrawNumber(hitNumber, 4);
        typeNode.Percent.text = BuildDrawNumber(hitPercentage, 3) + "%";
    }

    string BuildDrawNumber(int drawNumber, int digist, float opacity = 1.0f)
    {
        int max = (int)Mathf.Pow(10, digist) - 1;     // 1桁なら9, 2桁なら99, 3桁なら999, ... でカンスト。
        int value = Mathf.Max(Mathf.Min(drawNumber, max), 0);   // 丸める。
        return value.ToString().PadLeft(digist).Replace(' ', 'o');  // グレーの '0' は 'o' で描画できる（矩形リスト参照）。
    }
}
