using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNode : Node
{
    public string MusicPath { get; private set; }

    public MusicNode(string musicPath, Node parent)
    {
        MusicPath = musicPath;
        Parent = parent;
    }
}
