using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultStage : Stage
{
    public ResultStage()
    {
    }

    public override void OnOpen()
    {
        base.OnOpen();

        var playingNode = MainScript.Instance.MusicTree.FocusNode as MusicNode;

        if (playingNode.PreviewSprite)
            GetComponent<Image>("PreviewImage").sprite = playingNode.PreviewSprite;

        GetComponent<Text>("SongTitle/Title").text = playingNode.Title;
        GetComponent<Text>("SongTitle/SubTitle").text = playingNode.SubTitle;

        // show performance.
        var grade = MainScript.Instance.CurrentGrade;
        AddChild(new PerformanceDispaly(grade, FindChild("PerformanceDispaly").gameObject));
    }

    public override void Update()
    {
        base.Update();

        if (InputManager.Instance.HasOk())
        {
            SwitchManager.Instance.Open<ShutterSwitch>().OnSwitchMiddleClosed = () =>
            {
                StageManager.Instance.Open<SelectionStage>();
                Close();
            };
        }
    }
}
