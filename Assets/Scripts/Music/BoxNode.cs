using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class BoxNode : Node
{
    public BoxNode(string name, Node parent)
    {
        Title = name;
        Parent = parent;
    }

    public static BoxNode LoadFromFile(string filePath, Node parent)
    {
        string title = "", artist = "";
        using (var sr = new StreamReader(filePath, Encoding.GetEncoding(932/*Shift-JIS*/ )))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                try
                {
                    string parameter = "";
                    if (Utilities.ParseLineParams(line, @"TITLE", out parameter)) title = parameter;
                    else if (Utilities.ParseLineParams(line, @"ARTIST", out parameter)) artist = parameter;
                }
                catch
                {
                    // Ignore exception.
                }
            }
        }
        return new BoxNode(title, parent);
    }
}
