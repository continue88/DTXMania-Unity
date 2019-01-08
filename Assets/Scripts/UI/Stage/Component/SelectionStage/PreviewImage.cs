using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewImage : Activity
{
    Image mPreviewImage;
    Sprite mDefaultPreviewSprite;

    public PreviewImage(GameObject go)
        : base(go)
    {
    }

    public override void OnOpen()
    {
        base.OnOpen();

        // find the preview image.
        mPreviewImage = GetComponent<Image>("Image");
        mDefaultPreviewSprite = mPreviewImage.sprite;

        // register the handler.
        var musicTree = MainScript.Instance.MusicTree;
        musicTree.OnFocusNodeChanged += OnFocusNodeChanged;

        RefreshPreviewImage(musicTree.FocusNode);
    }

    /// <summary>
    /// this activity is closed.
    /// </summary>
    public override void OnClose()
    {
        base.OnClose();

        var musicTree = MainScript.Instance.MusicTree;
        musicTree.OnFocusNodeChanged -= OnFocusNodeChanged;
    }

    private void OnFocusNodeChanged(object sender, MusicTree.FocusNodeChangedArgs e)
    {
        RefreshPreviewImage(e.SelectedNode);
    }

    private void RefreshPreviewImage(Node node)
    {
        mPreviewImage.sprite = node?.PreviewSprite ?? mDefaultPreviewSprite;
    }
}
