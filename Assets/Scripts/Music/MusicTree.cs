using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class MusicTree
{
    public readonly static string[] SearchExtensions = { ".sstf", ".dtx", ".gda", ".g2d", "bms", "bme" };

    public RootNode Root { get; } = new RootNode();
    public Node FocusNode => FocusList?.SelectedItem;
    public SelectableList<Node> FocusList { get; private set; } = null;

    public event EventHandler<FocusNodeChangedArgs> OnFocusNodeChanged;

    public class FocusNodeChangedArgs
    {
        public Node SelectedNode;
        public Node DeselectNode;
    }

    public MusicNode AddMusicNode(string file, Node parentNode = null)
    {
        if (parentNode == null)
            parentNode = Root;

        try
        {
            var music = new MusicNode(file, parentNode);
            parentNode.ChildNodeList.Add(music);
            return music;
        }
        catch
        {
            Debug.LogError("Fail to load music file: " + file);
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

    private void FocusList_SelectionChanged(object sender, SelectableList<Node>.SelectChangedEventArgs e)
    {
        // 間接呼び出し；
        // フォーカスリストの SelectedChanged イベントハンドラ　→　このクラス内で変更されうる
        // 外部に対するイベントハンドラ　→　このクラス内では変更されない
        OnFocusNodeChanged?.Invoke(sender, new FocusNodeChangedArgs { SelectedNode = e.SelectItem, DeselectNode = e.DeselectItem });
    }
}
