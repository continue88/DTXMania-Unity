using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SetNode : Node
{
    public MusicNode[] MusicNodes = new MusicNode[5];

    public SetNode(SetDef.Block block, string baseFolder, Node parentNode)
    {
        Title = block.Title;
        Parent = parentNode;

        for (int i = 0; i < 5; i++)
        {
            MusicNodes[i] = null;
            if (string.IsNullOrEmpty(block.File[i]))
                continue;

            try
            {
                MusicNodes[i] = new MusicNode(Path.Combine(baseFolder, block.File[i]), this);
                Difficulty[i].Label = block.Label[i];
                ChildNodeList.Add(this.MusicNodes[i]);
            }
            catch
            {
                Debug.LogError("fail to load.");
            }
        }
    }

    private readonly string[] mThumbnailNames = { "thumb.png", "thumb.bmp", "thumb.jpg", "thumb.jpeg" };
}
