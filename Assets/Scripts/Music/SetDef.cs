using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class SetDef
{
    public class Block
    {
        public string Title { get; set; }
        public string[] File { get; set; }
        public Color FontColor { get; set; }
        public string Genre { get; set; }
        public string[] Label { get; set; }


        public Block()
        {
            Title = "";
            File = new string[5];
            FontColor = Color.white;
            Genre = "";
            Label = new string[5];
        }
    }

    public List<Block> Blocks = new List<Block>();


    public SetDef()
    {
    }

    public static SetDef RestoreFrom(string filePath)
    {
        var setDef = new SetDef();
        using (var sr = new StreamReader(filePath, Encoding.GetEncoding(932/*Shift-JIS*/ )))
        {
            string line;
            var block = new Block();
            var blockValid = false;

            while ((line = sr.ReadLine()) != null)
            {
                try
                {
                    string parameter = "";
                    if (Utilities.ParseLineParams(line, @"TITLE", out parameter))
                    {
                        if (blockValid)
                        {
                            FixBlockLevelLabels(block);
                            setDef.Blocks.Add(block);
                            block = new Block();
                        }
                        block.Title = parameter;
                        blockValid = true;
                    }
                    else if (Utilities.ParseLineParams(line, @"FONTCOLOR", out parameter))
                    {
                        var sysColor = Color.black;// System.Drawing.ColorTranslator.FromHtml($"#{parameter}");
                        block.FontColor = sysColor;// new Color(sysColor.r, sysColor.G, sysColor.B, sysColor.A);
                        blockValid = true;
                    }
                    else if (Utilities.ParseLineParams(line, @"L1FILE", out parameter))
                    {
                        block.File[0] = parameter;
                        blockValid = true;
                    }
                    else if (Utilities.ParseLineParams(line, @"L2FILE", out parameter))
                    {
                        block.File[1] = parameter;
                        blockValid = true;
                    }
                    else if (Utilities.ParseLineParams(line, @"L3FILE", out parameter))
                    {
                        block.File[2] = parameter;
                        blockValid = true;
                    }
                    else if (Utilities.ParseLineParams(line, @"L4FILE", out parameter))
                    {
                        block.File[3] = parameter;
                        blockValid = true;
                    }
                    else if (Utilities.ParseLineParams(line, @"L5FILE", out parameter))
                    {
                        block.File[4] = parameter;
                        blockValid = true;
                    }
                    else if (Utilities.ParseLineParams(line, @"L1LABEL", out parameter))
                    {
                        block.Label[0] = parameter;
                        blockValid = true;
                    }
                    else if (Utilities.ParseLineParams(line, @"L2LABEL", out parameter))
                    {
                        block.Label[1] = parameter;
                        blockValid = true;
                    }else if (Utilities.ParseLineParams(line, @"L3LABEL", out parameter))
                    {
                        block.Label[2] = parameter;
                        blockValid = true;
                    }
                    else if (Utilities.ParseLineParams(line, @"L4LABEL", out parameter))
                    {
                        block.Label[3] = parameter;
                        blockValid = true;
                    }
                    else if (Utilities.ParseLineParams(line, @"L5LABEL", out parameter))
                    {
                        block.Label[4] = parameter;
                        blockValid = true;
                    }
                }
                catch
                {
                    // 
                }
            }

            if (blockValid)
            {
                FixBlockLevelLabels(block);
                setDef.Blocks.Add(block);
            }
        }

        return setDef;
    }


    private static void FixBlockLevelLabels(Block block)
    {
        var levelLables = new string[] { "BASIC", "ADVANCED", "EXTREME", "MASTER", "ULTIMATE" };

        for (int i = 0; i < 5; i++)
        {
            if (!string.IsNullOrEmpty(block.File[i]) && string.IsNullOrEmpty(block.Label[i]))
                block.Label[i] = levelLables[i];
        }
    }
}
