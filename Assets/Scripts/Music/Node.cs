using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    protected Sprite mPreviewSprite;

    public Node Parent { get; protected set; } = null;
    public SelectableList<Node> ChildNodeList { get; } = new SelectableList<Node>();
    public DifficultyLabel[] Difficulty { get; } = new DifficultyLabel[MusicTree.MaxDiffLevel];
    public string PreviewImagePath { get; protected set; }
    public string PreviewAudioPath { get; protected set; }
    public string Title { get; protected set; } = "";
    public string SubTitle { get; protected set; }
    public virtual Sprite PreviewSprite { get { return mPreviewSprite; } }

    public class DifficultyLabel
    {
        public string Label;
        public float Level; // 0-9.99
    }

    public Node()
    {
        for (var i = 0; i < Difficulty.Length; i++)
            Difficulty[i] = new DifficultyLabel { Label = "", Level = 0.00f };
    }

    public void PlayPreviewAudio()
    {
        if (string.IsNullOrEmpty(PreviewAudioPath))
            return;

    }

    public void StopPreviewAudio()
    {

    }


    public Node PreNode
    {
        get
        {
            var index = Parent.ChildNodeList.IndexOf(this);
            index = index - 1;
            if (0 > index) index = Parent.ChildNodeList.Count - 1;
            return Parent.ChildNodeList[index];
        }
    }

    public Node NextNode
    {
        get
        {
            var index = Parent.ChildNodeList.IndexOf(this);
            index = index + 1;
            if (Parent.ChildNodeList.Count <= index) index = 0;
            return Parent.ChildNodeList[index];
        }
    }

    public void OnLoadPreviewImage(WWW www)
    {
        if (string.IsNullOrEmpty(www.error))
        {
            var loadedTextue = www.textureNonReadable;
            if (loadedTextue)
                mPreviewSprite = Sprite.Create(loadedTextue, new Rect(0, 0, loadedTextue.width, loadedTextue.height), new Vector2(0.5f, 0.5f));
            else
                Debug.LogError("The previou image not a valid texture: " + www.url);
        }
        else
        {
            Debug.LogError("Fail to load preview image from: " + www.url);
        }
    }
}
