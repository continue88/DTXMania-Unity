using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node Parent { get; set; } = null;
    public SelectableList<Node> ChildNodeList { get; } = new SelectableList<Node>();

    public string PreviewImagePath { get; protected set; } = "";
    public string PreviewAudioPath { get; protected set; } = "";
    public Sprite PreviewSprite { get; protected set; }
    public string Title { get; protected set; } = "(no title)";
    public string SubTitle { get; protected set; }

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
}
