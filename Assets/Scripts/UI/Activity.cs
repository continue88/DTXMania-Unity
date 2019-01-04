using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity
{
    GameObject mGameObject;

    public GameObject GameObject { get; set; }

    public Activity(GameObject go)
    {
        mGameObject = go;
    }

    public virtual void OnOpen()
    {

    }

    public virtual void Update()
    {

    }
}
