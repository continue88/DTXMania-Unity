using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node Parent { get; set; } = null;
    public SelectableList<Node> ChildNodeList { get; } = new SelectableList<Node>();
}
