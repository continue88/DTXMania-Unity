using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager
{
    public static StageManager Instance { get; private set; } = new StageManager();
    private StageManager() { }

    public Stage CurrentStage { get; private set; }

    public T Open<T>() where T : Stage, new()
    {
        var name = typeof(T).Name;
        var prefab = Resources.Load<GameObject>(name);
        if (!prefab)
        {
            Debug.LogError("Fail to load prefab data.");
            return null;
        }

        var uiRoot = MainScript.Instance.UIRoot;
        var instance = Object.Instantiate(prefab, uiRoot);

        var stage = new T();
        stage.GameObject = instance;
        stage.OnOpen();
        return stage;
    }

    public void Close()
    {
    }
}
