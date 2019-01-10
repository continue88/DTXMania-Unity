using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionStage : Stage
{
    SongList mSongList;
    PreviewImage mPreviewImage;

    public override void OnOpen()
    {
        base.OnOpen();

        mSongList = AddChild(new SongList(FindChild("SongList").gameObject));
        mPreviewImage = AddChild(new PreviewImage(FindChild("PreviewImage").gameObject));
    }

    public override void Update()
    {
        base.Update();

        UpdateInput();
    }

    void UpdateInput()
    {
        if (InputManager.Instance.HasMoveUp())
            MainScript.Instance.MusicTree.FocusPreviousNode();

        if (InputManager.Instance.HasMoveDown())
            MainScript.Instance.MusicTree.FocusNextNode();

        if (InputManager.Instance.HasOk())
        {
            var focusNode = MainScript.Instance.MusicTree.FocusNode;
            if (focusNode is MusicNode)
            {
                var musicNode = focusNode as MusicNode;
                MainScript.Instance.PlayingScore = musicNode.Score;
                StageManager.Instance.Open<SongLoadStage>();
                Close();
            }
        }
        if (InputManager.Instance.HasCancle())
        {
            StageManager.Instance.Open<LoginStage>();
            Close();
        }
    }
}
