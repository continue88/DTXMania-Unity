using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class MusicTree
{
    public readonly static string[] SearchExtensions = { ".sstf", ".dtx", ".gda", ".g2d", "bms", "bme" };

    public RootNode Root { get; } = new RootNode();

    public MusicNode AddMusicNode(string file, Node parentNode = null)
    {
        if (parentNode == null)
            parentNode = Root;

        var music = new MusicNode(file, parentNode);
        parentNode.ChildNodeList.Add(music);
        return music;
    }
}
