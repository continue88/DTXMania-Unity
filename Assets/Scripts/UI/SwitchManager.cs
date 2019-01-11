using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager
{
    public static SwitchManager Instance { get; private set; } = new SwitchManager();
    private SwitchManager() { }

    public Switch CurrentSwitch { get; private set; }

    /// <summary>
    /// open a stage.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T Open<T>() where T : Switch, new()
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

        var switchObj = new T();
        switchObj.GameObject = instance;

        CurrentSwitch = switchObj;

        switchObj.OnOpen();
        return switchObj;
    }

    public void Update()
    {
        CurrentSwitch?.Update();
    }

    public void Close(Switch stage)
    {
        if (stage == CurrentSwitch) CurrentSwitch = null;
        if (stage.GameObject) Object.Destroy(stage.GameObject);
        stage.GameObject = null;
        stage.OnClose();
    }
}
