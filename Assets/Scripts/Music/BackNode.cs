using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackNode : Node
{
    public BackNode(Node parent)
    {
        Title = "<< Back";
        Parent = parent;
    }
}
