using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class Utilities
{
    public static bool ParseLineParams(string line, string cmd, out string parameter)
    {
        line = line.Split(';')[0].Trim(' ', '\t');

        var regex = $@"^\s*#\s*{cmd}(:|\s)+(.*)\s*$";
        var m = Regex.Match(line, regex, RegexOptions.IgnoreCase);

        if (m.Success && (3 <= m.Groups.Count))
        {
            parameter = m.Groups[2].Value;
            return true;
        }
        else
        {
            parameter = null;
            return false;
        }
    }

    public static T GetComponent<T>(this Transform transform, string path) where T : Component
    {
        var child = transform.Find(path);
        if (!child) return null;
        return child.GetComponent<T>();
    }
}
