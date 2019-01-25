using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager
{
    public static LoadManager Instance { get; private set; } = new LoadManager();
    private LoadManager() { }

    public void LoadDirectory(string dir, Action<string> onLoadDir)
    {
        onLoadDir(dir);
    }
}
