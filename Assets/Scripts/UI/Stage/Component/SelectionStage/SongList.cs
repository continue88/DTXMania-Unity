using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongList : Activity
{
    int mCursorPos = 4;
    ScrollRect mScrollRect;
    RectTransform mItemTemplate;

    const int TotalSongItem = 10;
    const int ItemGrap = 10;
    const int SelectOffset = 20;

    public SongList(GameObject go)
        : base(go)
    {
        var musicTree = MainScript.Instance.MusicTree;
        musicTree.OnFocusNodeChanged += MusicTree_OnFocusNodeChanged;
        if (musicTree.FocusNode == null)
        {
            if (musicTree.Root.ChildNodeList.Count > 0)
                musicTree.FocusOn(musicTree.Root.ChildNodeList[0]);
        }
        else
        {
            musicTree.FocusNode.PlayPreviewAudio();
        }
    }

    public override void OnOpen()
    {
        base.OnOpen();

        // get the first song item template.
        mScrollRect = GameObject.GetComponent<ScrollRect>();
        mItemTemplate = mScrollRect.content.GetChild(0) as RectTransform;
        mItemTemplate.gameObject.SetActive(false);

        BuildSongList();
    }

    private void MusicTree_OnFocusNodeChanged(object sender, MusicTree.FocusNodeChangedArgs e)
    {
    }


    void BuildSongList()
    {
        var musicTree = MainScript.Instance.MusicTree;
        var nodeToShow = musicTree.FocusNode;
        if (nodeToShow == null) return;

        for (var i = 0; i < mCursorPos; i++)
            nodeToShow = nodeToShow.PreNode;

        for (var i = 0; i < TotalSongItem; i++)
        {
            BuildSongItem(nodeToShow, i);
            nodeToShow = nodeToShow.NextNode;
        }
    }

    /// <summary>
    /// build a song item, setup it title, sub title, and preview image.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="index"></param>
    void BuildSongItem(Node node, int index)
    {
        var itemHeight = mItemTemplate.sizeDelta.y;
        var songItem = Object.Instantiate(mItemTemplate.gameObject, mItemTemplate.parent).transform as RectTransform;
        var itemPosX = (index == mCursorPos) ? 0 : SelectOffset;
        var itemPosY = (TotalSongItem / 2 - index - 1) * (itemHeight + ItemGrap);
        songItem.localPosition = new Vector3(itemPosX, itemPosY, 0);
        songItem.gameObject.SetActive(true);

        // setup title text.
        songItem.Find("TextTitle").GetComponent<Text>().text = node.Title;
        songItem.Find("TextSubTitle").GetComponent<Text>().text = node.SubTitle;
    }
}
