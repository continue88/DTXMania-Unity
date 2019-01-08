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
        if (InputManager.Instance.HasOk())
        {
            StageManager.Instance.Open<SongLoadStage>();

            Close();
        }
    }
}
