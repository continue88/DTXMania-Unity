using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Activity
{
    public Switch()
        : base(null)
    {

    }

    public void Close()
    {
        SwitchManager.Instance.Close(this);
    }
}
