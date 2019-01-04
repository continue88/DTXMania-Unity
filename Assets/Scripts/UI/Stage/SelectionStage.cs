﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionStage : Stage
{
    public override void Update()
    {
        base.Update();

        if (InputManager.Instance.HasOk())
        {
            StageManager.Instance.Open<SongLoadStage>();

            Close();
        }
    }
}