using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionStage : Stage
{
    SongList mSongList;

    public override void OnOpen()
    {
        base.OnOpen();

        mSongList = AddChild(new SongList(FindChild("SongList").gameObject));
    }

    public override void Update()
    {
        base.Update();

        if (InputManager.Instance.HasOk())
        {
            StageManager.Instance.Open<SongLoadStage>();

            Close();
        }
    }
}
