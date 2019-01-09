using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity
{
    GameObject mGameObject;

    public Activity Parent { get; private set; } = null;
    public GameObject GameObject { get { return mGameObject; } set { mGameObject = value; Transform = mGameObject?.transform; } }
    public Transform Transform { get; private set; }

    protected List<Activity> ChildList { get; private set; } = new List<Activity>();

    public Activity(GameObject go)
    {
        GameObject = go;
    }

    public virtual void OnOpen()
    {
        for (var i = 0; i < ChildList.Count; i++)
            ChildList[i].OnOpen();
    }

    public virtual void OnClose()
    {
        for (var i = 0; i < ChildList.Count; i++)
            ChildList[i].OnClose();
        ChildList.Clear();
    }

    public virtual void Update()
    {
        for (var i = 0; i < ChildList.Count; i++)
            ChildList[i].Update();
    }

    protected void StartCoroutine(IEnumerator corutin)
    {
        MainScript.Instance.StartCoroutine(corutin);
    }

    public Transform FindChild(string path)
    {
        if (!Transform) return null;
        if (string.IsNullOrEmpty(path)) return Transform;
        return Transform.Find(path);
    }

    public T GetComponent<T>(string path) where T : Component
    {
        var child = FindChild(path);
        if (!child) return null;
        return child.GetComponent<T>();
    }

    protected T AddChild<T>(T childActivity, bool fireOnOpen = true) where T : Activity
    {
        childActivity.Parent = this;
        ChildList.Add(childActivity);
        if (fireOnOpen) childActivity.OnOpen(); // calling it here?
        return childActivity;
    }

    protected T RemoveChild<T>(T childActivity, bool fireOnClose = false) where T : Activity
    {
        if (fireOnClose) childActivity.OnClose();
        ChildList.Remove(childActivity);
        childActivity.Parent = null;
        return childActivity;
    }
}
