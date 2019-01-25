using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SetNode : Node
{
    private static readonly string[] ThumbnailNames = { "thumb.png", "thumb.bmp", "thumb.jpg", "thumb.jpeg" };

    public MusicNode[] MusicNodes = new MusicNode[MusicTree.MaxDiffLevel];

    public SetNode(SetDef.Block block, string baseFolder, Node parentNode)
    {
        Title = block.Title;
        Parent = parentNode;

        for (int i = 0; i < MusicNodes.Length; i++)
        {
            MusicNodes[i] = null;
            if (string.IsNullOrEmpty(block.File[i]))
                continue;

            try
            {
                var fullPath = Path.Combine(baseFolder, block.File[i]);
                if (!File.Exists(fullPath)) continue;

                MusicNodes[i] = new MusicNode(fullPath, this);
                Difficulty[i].Label = block.Label[i];
                ChildNodeList.Add(this.MusicNodes[i]);

                if (string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(MusicNodes[i].Title))
                    Title = MusicNodes[i].Title;
            }
            catch (System.Exception ex)
            {
                Debug.LogError("fail to load. Exception:" + ex);
            }
        }

        for (var i = 0; i < ThumbnailNames.Length; i++)
        {
            var fullPath = Path.Combine(baseFolder, ThumbnailNames[i]);
            if (File.Exists(fullPath))
            {
                PreviewImagePath = fullPath;
                break;
            }
        }
    }

    public MusicNode GetSelectMusicNode()
    {
        var musicTree = MainScript.Instance.MusicTree;
        return MusicNodes[musicTree.GetClosestDifficultyLevel(this)];
    }

    public override Sprite PreviewSprite
    {
        get
        {
            var musicTree = MainScript.Instance.MusicTree;
            var currentDiff = MusicNodes[musicTree.FocusDifficulty];
            if (null != currentDiff?.PreviewSprite)
                return currentDiff.PreviewSprite;

            if (mPreviewSprite) return mPreviewSprite;

            return MusicNodes[musicTree.GetClosestDifficultyLevel(this)].PreviewSprite;
        }
    }
}
