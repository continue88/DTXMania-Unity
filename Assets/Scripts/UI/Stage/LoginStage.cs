using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginStage : Stage
{
    bool mPressedCancel = false;
    Transform mUserList;

    public override void OnOpen()
    {
        base.OnOpen();

        mUserList = FindChild("UserList");
        if (UserManager.Instance.LoggedOnUser == null)
            UserManager.Instance.UserList.SelectFirst();

        mUserList.GetChild(UserManager.Instance.UserList.SelectedIndex).GetComponent<Toggle>().isOn = true;
    }

    public override void Update()
    {
        base.Update();

        if (InputManager.Instance.HasMoveUp())
        {
            if (UserManager.Instance.UserList.SelectPrev())
                mUserList.GetChild(UserManager.Instance.UserList.SelectedIndex).GetComponent<Toggle>().isOn = true;
        }
        if (InputManager.Instance.HasMoveDown())
        {
            if (UserManager.Instance.UserList.SelectNext())
                mUserList.GetChild(UserManager.Instance.UserList.SelectedIndex).GetComponent<Toggle>().isOn = true;
        }

        if (InputManager.Instance.HasOk())
        {
            StageManager.Instance.Open<SelectionStage>();
            Close();
        }

        if (InputManager.Instance.HasCancle())
        {
            if (!mPressedCancel) mPressedCancel = true;
            else
                Application.Quit();
        }
    }
}
