using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongInfo : Activity
{
    public SongInfo(GameObject gameObject)
        : base(gameObject)
    {
    }

    public override void OnOpen()
    {
        base.OnOpen();

        var playingNode = MainScript.Instance.MusicTree.FocusNode as MusicNode;

        var previewImage = GetComponent<Image>("Preview");
        var previewSprite = playingNode.PreviewSprite;
        if (previewImage && previewSprite)
            previewImage.sprite = previewSprite;

        GetComponent<Text>("TextTitle").text = playingNode.Title;
        GetComponent<Text>("TextArtist").text = playingNode.SubTitle;
    }
}
