using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental;
using System.Linq;
using System.IO;
using System.Xml;
using UnityEditor.Experimental.U2D;

public static class EditorTools
{
    static readonly char[] Sperator = ": [,]".ToCharArray();

    [MenuItem("DTXMania/ConvertSprites")]
    static void ConvertSprites()
    {
        var assetPath = Application.dataPath + "/Images";

        var yaml = Directory.GetFiles(assetPath, "*.yaml", SearchOption.AllDirectories).Count(path => ConvertYaml(path));
        Debug.Log($"Converted { yaml} yaml files.");

        var xml = Directory.GetFiles(assetPath, "*.xml", SearchOption.AllDirectories).Count(path => ConvertXml(path));
        Debug.Log($"Converted { xml } xml files.");
    }

    static bool ConvertYaml(string path)
    {
        // LeftCymbal: [1, 0, 91, 98]
        Debug.Log("ConvertYaml: " + path);
        var subImages = File.ReadAllLines(path)
            .Select(line => line.Trim())
            .Where(line => line.Contains(':') && line[0] != '#' && line[line.Length - 1] == ']')
            .Select(line =>
            {
                var sheet = new SpriteRect();
                var start = 0;
                var flag = line[start];
                var end = line.LastIndexOf(':');
                if (flag == '\'' || flag == '\"')
                {
                    start = 1;
                    end = line.LastIndexOf(flag);
                }
                sheet.name = line.Substring(start, end - start);
                if (string.IsNullOrEmpty(sheet.name) || sheet.name == "." || sheet.name == "/") sheet.name = "\\" + sheet.name;

                var args = line.Substring(line.LastIndexOf('[')).Split(Sperator, System.StringSplitOptions.RemoveEmptyEntries);
                sheet.rect = new Rect(int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2]), int.Parse(args[3]));
                return sheet;
            }).ToArray();
        return ConvertSprite(path, subImages);
    }

    static bool ConvertXml(string path)
    {
        try
        {
            Debug.Log("ConvertXml: " + path);
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            var subImages = xmlDoc.GetElementsByTagName("SubImage")
                .OfType<XmlElement>()
                .Select(element => 
                {
                    var rect = new SpriteRect();
                    rect.name = element.GetAttribute("Name");
                    var args = element.GetAttribute("Rectangle").Split(',');
                    rect.rect = new Rect(int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2]), int.Parse(args[3]));
                    return rect;
                }).ToArray();
            return ConvertSprite(path, subImages);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Faile to load: " + path + ", Error: " + ex.Message);
            return false;
        }
    }

    static bool ConvertSprite(string path, SpriteRect[] spritesheet)
    {
        var assetPath = path.Substring(path.IndexOf("Assets"));
        assetPath = assetPath.Substring(0, assetPath.Length - ".yaml".Length) + ".png";
        Debug.Log("AssetPath=" + assetPath);
        var ai = AssetImporter.GetAtPath(assetPath) as ISpriteEditorDataProvider;
        if (ai == null) return false;

        ai.InitSpriteEditorDataProvider();
        var textureProvider = ai.GetDataProvider<ITextureDataProvider>();
        if (textureProvider != null)
        {
            int width = 0, height = 0;
            textureProvider.GetTextureActualWidthAndHeight(out width, out height);
            spritesheet.Select(x => x.rect = new Rect(x.rect.x, height - x.rect.y - x.rect.height, x.rect.width, x.rect.height)).Count();
        }

        ai.SetSpriteRects(spritesheet);
        ai.Apply();

        var importer = ai as TextureImporter;
        if (importer)
        {
            importer.spriteImportMode = SpriteImportMode.Multiple;
            importer.SaveAndReimport();
        }

        return true;
    }
}
