using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongList : Activity
{
    int mCursorPos = 4;
    bool mMoveDown = true;
    RectTransform mItemTemplate;
    UIAnimation mUIAnimation = null;

    const int TotalSongItem = 10;
    const int ItemGrap = 10;
    const int SelectOffset = 20;

    public SongList(GameObject go)
        : base(go)
    {
    }

    public override void OnOpen()
    {
        base.OnOpen();
        
        // get the first song item template.
        mItemTemplate = Transform.Find("Content").GetChild(0) as RectTransform;
        mItemTemplate.gameObject.SetActive(false);

        // get the focuse node, select on if not presented.
        var musicTree = MainScript.Instance.MusicTree;
        if (musicTree.FocusNode == null)
        {
            if (musicTree.Root.ChildNodeList.Count > 0)
                musicTree.FocusOn(musicTree.Root.ChildNodeList[0]);
        }
        else
        {
            musicTree.FocusNode.PlayPreviewAudio();
        }
        musicTree.OnFocusNodeChanged += OnFocusNodeChanged;

        // setup the select animation.
        mUIAnimation = new UIAnimation
        {
            duration = 0.4f,
            OnUpdate = (factor, finished) =>
            {
                factor *= 2.0f;
                if (factor < 1)
                {
                    var startOffset = (mItemTemplate.sizeDelta.y + ItemGrap);
                    if (mMoveDown) startOffset *= -1;
                    mItemTemplate.parent.localPosition = new Vector3(0, startOffset * (1 - factor));
                }
                else
                {
                    factor -= 1.0f;
                    mItemTemplate.parent.localPosition = Vector3.zero;
                    if (mItemTemplate.parent.childCount > mCursorPos)
                    {
                        var focusItem = mItemTemplate.parent.GetChild(mCursorPos);
                        focusItem.localPosition = new Vector3(SelectOffset * (1 - factor), 0);
                    }
                }
            },
            onFinished = () =>
            {
                mItemTemplate.parent.localPosition = Vector3.zero;
                if (mCursorPos < mItemTemplate.parent.childCount)
                {
                    var focusItem = mItemTemplate.parent.GetChild(mCursorPos);
                    focusItem.localPosition = new Vector3(0, 0);
                }
            }
        };

        RefreshSongList();
    }

    public override void Update()
    {
        base.Update();
        mUIAnimation?.Update();
    }

    public override void OnClose()
    {
        base.OnClose();

        var musicTree = MainScript.Instance.MusicTree;
        musicTree.OnFocusNodeChanged -= OnFocusNodeChanged;
    }

    private void OnFocusNodeChanged(object sender, MusicTree.FocusNodeChangedArgs e)
    {
        RefreshSongList();

        mMoveDown = e.DeselectNode == e.SelectedNode.PreNode;
        mUIAnimation?.ResetToBeginning();
        mUIAnimation?.PlayForward();
    }

    /// <summary>
    /// RefreshSongList
    /// </summary>
    void RefreshSongList()
    {
        var musicTree = MainScript.Instance.MusicTree;
        var nodeToShow = musicTree.FocusNode;
        if (nodeToShow == null) return;

        for (var i = 0; i < mCursorPos; i++)
            nodeToShow = nodeToShow.PreNode;

        // show the shong list.
        for (var i = 0; i < TotalSongItem; i++)
        {
            BuildSongItem(nodeToShow, i);
            nodeToShow = nodeToShow.NextNode;
        }

        // try to play the preview audio.
        MainScript.Instance.WAVManager.PlaySound(nodeToShow.PreviewAudioPath, true);
    }

    /// <summary>
    /// build a song item, setup it title, sub title, and preview image.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="index"></param>
    void BuildSongItem(Node node, int index)
    {
        var itemHeight = mItemTemplate.sizeDelta.y;
        var songItem = mItemTemplate.parent.childCount > index ?
            mItemTemplate.parent.GetChild(index) as RectTransform :
            Object.Instantiate(mItemTemplate.gameObject, mItemTemplate.parent).transform as RectTransform;
        var itemPosY = (TotalSongItem / 2 - index - 1) * (itemHeight + ItemGrap);
        songItem.localPosition = new Vector3(SelectOffset, itemPosY, 0);
        songItem.gameObject.SetActive(true);

        // setup title text.
        songItem.Find("TextTitle").GetComponent<Text>().text = node.Title;
        songItem.Find("TextSubTitle").GetComponent<Text>().text = node.SubTitle;

        // setup image thumbnail.
        if (node.PreviewSprite)
            songItem.Find("ImageThumbnail").GetComponent<Image>().sprite = node.PreviewSprite;
    }
}
