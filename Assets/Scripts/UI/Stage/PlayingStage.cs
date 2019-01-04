using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingStage : Stage
{
    public override void Update()
    {
        base.Update();

        if (InputManager.Instance.HasCancle())
        {
            StageManager.Instance.Open<SelectionStage>();

            Close();
        }
    }
}
