using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager
{
    public static StageManager Instance { get; private set; } = new StageManager();
    private StageManager() { }

    public Stage CurrentStage { get; private set; }

    /// <summary>
    /// open a stage.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T Open<T>() where T : Stage, new()
    {
        var name = typeof(T).Name;
        var prefab = Resources.Load<GameObject>(name);
        if (!prefab)
        {
            Debug.LogError("Fail to load prefab data: " + name);
            return null;
        }

        var uiRoot = MainScript.Instance.UIRoot;
        var instance = Object.Instantiate(prefab, uiRoot);

        var stage = new T();
        stage.GameObject = instance;

        CurrentStage = stage;

        stage.OnOpen();
        return stage;
    }

    public void Update()
    {
        if (CurrentStage != null)
            CurrentStage.Update();
    }

    public void Close(Stage stage)
    {
        if (stage == CurrentStage) CurrentStage = null;
        if (stage.GameObject) Object.Destroy(stage.GameObject);
        stage.GameObject = null;
        stage.OnClose();
    }
}
