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
            mSongList.SelectPrevious();

        if (InputManager.Instance.HasMoveDown())
            mSongList.SelectNext();

        if (InputManager.Instance.HasOk())
        {
            var focusNode = MainScript.Instance.MusicTree.FocusNode;
            if (focusNode is BoxNode)
                mSongList.IntoBox();
            else if (focusNode is BackNode)
                mSongList.OutofBox();
            else if (focusNode != null)
                PlayingMusicNode(focusNode);
        }

        if (InputManager.Instance.HasCancle())
        {
            var focusNode = MainScript.Instance.MusicTree.FocusNode;
            if (focusNode != null && focusNode.Parent != MainScript.Instance.MusicTree.Root)
                mSongList.OutofBox();
            else
            {
                StageManager.Instance.Open<LoginStage>();
                Close();
            }
        }
    }

    void PlayingMusicNode(Node focusNode)
    {
        // stop the previous audio sound.
        MainScript.Instance.WAVManager.Stop();

        // start fading to the song load stage.
        if (focusNode is MusicNode)
            MainScript.Instance.PlayingScore = ((MusicNode)focusNode).Score;
        else if (focusNode is SetNode)
        {
            var setNode = focusNode as SetNode;
            MainScript.Instance.PlayingScore = setNode.GetSelectMusicNode().Score;
        }

        SwitchManager.Instance.Open<ReadyPlayGo>().OnSwitchMiddleClosed = () =>
        {
            StageManager.Instance.Open<SongLoadStage>();
            Close();
        };
    }
}
