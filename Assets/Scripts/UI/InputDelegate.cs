using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDelegate : MonoBehaviour
{
    public void OnOK()
    {
        ManualyInputKey(DrumInputType.LeftCrash);
    }

    public void OnMoveUp()
    {
        ManualyInputKey(DrumInputType.Tom1);
    }

    public void OnMoveDown()
    {
        ManualyInputKey(DrumInputType.Tom2);
    }

    public void ManualyInputKey(DrumInputType drumInputType)
    {
        InputManager.Instance.EnqueueDrumInputEvent(new InputManager.DrumInputEvent
        {
            Type = drumInputType
        });
    }
}
