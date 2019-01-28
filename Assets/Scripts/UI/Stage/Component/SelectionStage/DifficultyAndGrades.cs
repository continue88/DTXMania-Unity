using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyAndGrades : Activity
{
    public DifficultyAndGrades(GameObject go)
        : base(go)
    {
    }

    public override void OnOpen()
    {
        base.OnOpen();

        MainScript.Instance.MusicTree.OnFocusNodeChanged += MusicTree_OnFocusNodeChanged;

        RefreshFocusNodeInfo();
    }

    public override void OnClose()
    {
        base.OnClose();

        MainScript.Instance.MusicTree.OnFocusNodeChanged -= MusicTree_OnFocusNodeChanged;
    }

    private void MusicTree_OnFocusNodeChanged(object sender, MusicTree.FocusNodeChangedArgs e)
    {
        RefreshFocusNodeInfo();
    }

    void RefreshFocusNodeInfo()
    {
        var levelsNode = Transform.Find("Levels");

        var node = MainScript.Instance.MusicTree.FocusNode;
        var level = MainScript.Instance.MusicTree.FocusDifficulty;
        if (node != null)
        {
            for (var i = 0; i < levelsNode.childCount && i < node.Difficulty.Length; i++)
                SetupDiffNode(levelsNode.GetChild(i), node.Difficulty[i], i == level);
        }
    }

    void SetupDiffNode(Transform node, Node.DifficultyLabel difficultyLabel, bool selected)
    {
        var levelExist = !string.IsNullOrEmpty(difficultyLabel.Label);

        node.GetComponent<Text>("TitleLabel/Text").text = difficultyLabel.Label;

        var value = difficultyLabel.Level;
        var intLabel = (int)value;
        var facLabel = (int)((value - intLabel) * 100);
        node.GetComponent<Text>("TextInt").text = levelExist ? intLabel + "." : "";
        node.GetComponent<Text>("TextFrac").text = levelExist ? facLabel.ToString() : "";

        node.Find("Selected").gameObject.SetActive(selected);
    }
}
