using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginStage : Stage
{
    bool mLastToogle = false;
    bool mPressedCancel = false;
    Transform mUserList;

    public override void OnOpen()
    {
        base.OnOpen();

        mUserList = FindChild("UserList");
        for (var i = 0; i < mUserList.childCount; i++)
            mUserList.GetChild(i).GetComponent<Toggle>().onValueChanged.AddListener(OnToggleValueChanged);

        if (UserManager.Instance.LoggedOnUser == null)
            UserManager.Instance.UserList.SelectFirst();

        ToogleSelectedUser();
    }

    public override void Update()
    {
        base.Update();

        if (InputManager.Instance.HasMoveUp())
        {
            if (UserManager.Instance.UserList.SelectPrev())
                ToogleSelectedUser();
        }
        if (InputManager.Instance.HasMoveDown())
        {
            if (UserManager.Instance.UserList.SelectNext())
                ToogleSelectedUser();
        }

        if (InputManager.Instance.HasOk())
            OnOK();

        if (InputManager.Instance.HasCancle())
        {
            if (!mPressedCancel) mPressedCancel = true;
            else
                Application.Quit();
        }
    }

    void ToogleSelectedUser()
    {
        mUserList.GetChild(UserManager.Instance.UserList.SelectedIndex).GetComponent<Toggle>().isOn = true;
    }

    void OnToggleValueChanged(bool selected)
    {
        if (!selected) return; // ignore the disable one.
        var selectedIndex = UserManager.Instance.UserList.SelectedIndex;
        for (var i = 0; i < mUserList.childCount; i++)
        {
            var userToggle = mUserList.GetChild(i).GetComponent<Toggle>();
            if (userToggle.isOn)
            {
                Debug.Log("Select item: " + i);
                UserManager.Instance.UserList.SelectItem(i);
                break;
            }
        }
    }

    void OnOK()
    {
        SwitchManager.Instance.Open<HalfTurnBlackFade>().OnSwitchMiddleClosed = () =>
        {
            StageManager.Instance.Open<SelectionStage>();
            Close();
        };

    }
}
