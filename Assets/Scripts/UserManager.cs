using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager
{
    public static UserManager Instance { get; private set; } = new UserManager();
    private UserManager()
    {
        UserList.Add(new UserSettings
        {
            Id = "AutoPlayer",
            Name = "AutoPlayer"
        });
        UserList.Add(new UserSettings
        {
            Id = "Guest",
            Name = "Guest",
            AutoPlay_LeftCymbal = false,
            AutoPlay_HiHat = false,
            AutoPlay_LeftPedal = false,
            AutoPlay_Snare = false,
            AutoPlay_Bass = false,
            AutoPlay_HighTom = false,
            AutoPlay_LowTom = false,
            AutoPlay_FloorTom = false,
            AutoPlay_RightCymbal = false,
        });
    }

    public SelectableList<UserSettings> UserList { get; protected set; } = new SelectableList<UserSettings>();
    public UserSettings LoggedOnUser => UserList.SelectedItem;
}
