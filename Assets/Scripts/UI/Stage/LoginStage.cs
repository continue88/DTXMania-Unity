using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginStage : Stage
{
    public override void OnOpen()
    {
        base.OnOpen();

        UserManager.Instance.UserList.SelectFirst();
    }

    public override void Update()
    {
        base.Update();

        if (InputManager.Instance.HasOk())
        {
            StageManager.Instance.Open<SelectionStage>();

            Close();
        }
    }
}
