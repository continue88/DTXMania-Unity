﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : Activity
{
    public Stage()
        : base(null)
    {

    }

    public void Close()
    {
        StageManager.Instance.Close(this);
    }
}
