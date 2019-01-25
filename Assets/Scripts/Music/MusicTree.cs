using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class MusicTree
{
    public readonly static string[] SearchExtensions = { ".sstf", ".dtx", ".gda", ".g2d", "bms", "bme" };

    public int DifficultyLevel { get; private set; }
    public RootNode Root { get; } = new RootNode();
    public Node FocusNode => FocusList?.SelectedItem;
    public SelectableList<Node> FocusList { get; private set; } = null;

    public event EventHandler<FocusNodeChangedArgs> OnFocusNodeChanged;

    public class FocusNodeChangedArgs
    {
        public Node SelectedNode;
        public Node DeselectNode;
    }

    public void SearchAndAddToParentNode(Node parentNode, string folder, Action<string> onFileDetected = null)
    {
        if (!Directory.Exists(folder)) return;

        var dirInfo = new DirectoryInfo(folder);
        var setDefPath = Path.Combine(folder, @"set.def");
        if (File.Exists(setDefPath))
        {
            onFileDetected?.Invoke(setDefPath);

            var setDef = SetDef.RestoreFrom(setDefPath);
            foreach (var block in setDef.Blocks)
            {
                var setNode = new SetNode(block, folder, parentNode);

                if (0 < setNode.ChildNodeList.Count) 
                    parentNode.ChildNodeList.Add(setNode);
            }
            return;
        }
        else
        {
            //----------------
            var fileInfos = dirInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly)
                .Where((fileInfo) => SearchExtensions.Any(ext => (Path.GetExtension(fileInfo.Name).ToLower() == ext)));
            var anyDtxFile = false;
            foreach (var fileInfo in fileInfos)
            {
                var vpath = fileInfo.FullName;
                onFileDetected?.Invoke(vpath);

                try
                {
                    var music = new MusicNode(vpath, parentNode);
                    parentNode.ChildNodeList.Add(music);
                    anyDtxFile = true;
                }
                catch
                {
                }
            }
            if (anyDtxFile) return;
        }

        foreach (var subDirInfo in dirInfo.GetDirectories())
        {
            var DTXFILES = "dtxfiles.";
            var boxDefPath = Path.Combine(subDirInfo.FullName, @"box.def");
            if (subDirInfo.Name.ToLower().StartsWith(DTXFILES))
            {
                var boxNode = new BoxNode(subDirInfo.Name.Substring(DTXFILES.Length), parentNode);
                parentNode.ChildNodeList.Add(boxNode);

                var backNode = new BackNode(boxNode);
                boxNode.ChildNodeList.Add(backNode);

                SearchAndAddToParentNode(boxNode, subDirInfo.FullName, onFileDetected);
            }
            else if (File.Exists(boxDefPath))
            {
                var boxNode = new BoxNode(boxDefPath, parentNode);
                parentNode.ChildNodeList.Add(boxNode);

                var backNode = new BackNode(boxNode);
                boxNode.ChildNodeList.Add(backNode);

                SearchAndAddToParentNode(boxNode, subDirInfo.FullName, onFileDetected);
            }
            else
            {
                SearchAndAddToParentNode(parentNode, subDirInfo.FullName, onFileDetected);
            }
        }
    }

    /// <summary>
    /// load a music node from a www dtx file.
    /// </summary>
    /// <param name="www"></param>
    /// <param name="parentNode"></param>
    /// <returns></returns>
    public MusicNode LoadMusicNode(WWW www, Node parentNode = null)
    {
        if (parentNode == null)
            parentNode = Root;

        try
        {
            if (string.IsNullOrEmpty(www.error))
            {
                using (var stream = new MemoryStream(www.bytes))
                {
                    var music = new MusicNode(www.url, parentNode, stream);
                    parentNode.ChildNodeList.Add(music);
                    return music;
                }   
            }
            else
            {
                Debug.LogError("Fail to load www data: " + www.url + "\nError:" + www.error);
                return null;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Fail to load music file: " + www.url + "\nmessage=" + ex.Message + "\ntrace:\n" + ex.StackTrace);
            return null;
        }
    }

    /// <summary>
    /// focus on a node.
    /// </summary>
    /// <param name="node"></param>
    public void FocusOn(Node node)
    {
        var parent = node.Parent ?? Root;
        var oldFocusList = FocusList;
        FocusList = parent.ChildNodeList;
        if (oldFocusList == FocusList)
        {
            FocusList.SelectItem(node);
        }
        else
        {
            if (oldFocusList != null)
                oldFocusList.SelectionChanged -= FocusList_SelectionChanged;

            if (node != null)
                FocusList.SelectItem(node);

            FocusList.SelectionChanged += FocusList_SelectionChanged;

            OnFocusNodeChanged?.Invoke(FocusList, new FocusNodeChangedArgs { SelectedNode = FocusList.SelectedItem, DeselectNode = oldFocusList?.SelectedItem });
        }
    }

    /// <summary>
    /// on the focus item changed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FocusList_SelectionChanged(object sender, SelectableList<Node>.SelectChangedEventArgs e)
    {
        OnFocusNodeChanged?.Invoke(sender, new FocusNodeChangedArgs { SelectedNode = e.SelectItem, DeselectNode = e.DeselectItem });
    }

    /// <remarks>
    ///		FocusNextNode。
    /// </remarks>
    public void FocusNextNode()
    {
        if (FocusList == null) return;
        var index = FocusList.SelectedIndex;
        index = (index + 1) % FocusList.Count;
        FocusList.SelectItem(index);
    }

    /// <remarks>
    ///		FocusPreviousNode。
    /// </remarks>
    public void FocusPreviousNode()
    {
        if (FocusList == null) return;
        var index = FocusList.SelectedIndex;
        index = (index - 1 + this.FocusList.Count) % FocusList.Count;
        FocusList.SelectItem(index);
    }

    public void IncreaseDifficulty()
    {
        for (int i = 0; i < 5; i++)   // 最大でも5回まで
        {
            DifficultyLevel = (DifficultyLevel + 1) % 5;

            if (FocusNode is SetNode)
            {
                //if (null != setnode.MusicNodes[this.mDifficultyLevel])
                //    return; // その難易度に対応する曲ノードがあればOK。
            }
        }
    }
}
