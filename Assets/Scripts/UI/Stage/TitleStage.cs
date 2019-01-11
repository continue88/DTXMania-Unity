using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleStage : Stage
{
    public override void Update()
    {
        base.Update();

        if (InputManager.Instance.HasOk())
        {
            SwitchManager.Instance.Open<ShutterSwitch>().OnSwitchMiddleClosed = () =>
            {
                StageManager.Instance.Open<LoginStage>();
                Close();
            };
        }
    }
}
