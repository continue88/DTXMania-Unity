using SSTFormat.v4;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public static InputManager Instance { get; private set; } = new InputManager();
    private InputManager() { }

    KeyBindings mKeyBindings = new KeyBindings();
    List<DrumInputEvent> mDrumInputEvents = new List<DrumInputEvent>();

    public class DrumInputEvent
    {
        public int DeviceID;
        public int Key;
        public DrumInputType Type;
        public bool Processed = false;
    }

    public bool HasMoveUp(bool checkDrumInput = true)
    {
        if (!CheckingInput()) return false;
        if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0) return true;
        if (checkDrumInput && HasAnyDrumInput(
            DrumInputType.Tom1, 
            DrumInputType.Tom1_Rim)) return true;
        return false;
    }
    public bool HasMoveDown(bool checkDrumInput = true)
    {
        if (!CheckingInput()) return false;
        if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0) return true;
        if (checkDrumInput && HasAnyDrumInput(
            DrumInputType.Tom2, 
            DrumInputType.Tom2_Rim)) return true;
        return false;
    }
    public bool HasMoveRight(bool checkDrumInput = true)
    {
        if (!CheckingInput()) return false;
        if (Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") > 0) return true;
        if (checkDrumInput && HasAnyDrumInput(
            DrumInputType.Tom3, 
            DrumInputType.Tom3_Rim)) return true;
        return false;
    }
    public bool HasMoveLeft(bool checkDrumInput = true)
    {
        if (!CheckingInput()) return false;
        if (Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") < 0) return true;
        if (checkDrumInput && HasAnyDrumInput(
            DrumInputType.Snare, 
            DrumInputType.Snare_ClosedRim, 
            DrumInputType.Snare_OpenRim)) return true;
        return false;
    }
    public bool HasOk(bool checkDrumInput = true)
    {
        if (!CheckingInput()) return false;
        if (Input.GetButtonDown("Submit")) return true;
        if (checkDrumInput && HasAnyDrumInput(
            DrumInputType.LeftCrash,
            DrumInputType.RightCrash,
            DrumInputType.China,
            DrumInputType.Ride,
            DrumInputType.Splash)) return true;
        return false;
    }
    public bool HasCancle(bool checkDrumInput = true)
    {
        if (!CheckingInput()) return false;
        if (Input.GetButtonDown("Cancel")) return true;
        if (checkDrumInput && HasAnyDrumInput(
            DrumInputType.Tom3,
            DrumInputType.Tom3_Rim)) return true;
        return false;
    }

    /// <summary>
    /// get the checing input
    /// </summary>
    /// <returns></returns>
    public bool CheckingInput()
    {
        // ignore the input if we are in a switching.
        if (SwitchManager.Instance.CurrentSwitch != null)
            return false;
        return true;
    }

    public IReadOnlyList<DrumInputEvent> DrumInputEvents { get { return mDrumInputEvents; } }

    public void PollAllInputDevices()
    {
        mDrumInputEvents.Clear();

        // check for the keyboard messages.
        foreach (var idKey in mKeyBindings.KeyboardToDrum)
        {
            if (Input.GetKeyDown((KeyCode)idKey.Key.Key))
                mDrumInputEvents.Add(new DrumInputEvent { DeviceID = idKey.Key.DeviceId, Key = idKey.Key.Key, Type = idKey.Value });
        }

        // check for the midi input device.
        var midiDriver = MainScript.Instance.UsbMidiDriver;
        if (midiDriver)
        {
            foreach (var idKey in mKeyBindings.MIDItoDrum)
            {
                if (idKey.Key.DeviceId == 0 && midiDriver.GetMidiNoteOn(idKey.Key.Key))
                    mDrumInputEvents.Add(new DrumInputEvent { DeviceID = idKey.Key.DeviceId, Key = idKey.Key.Key, Type = idKey.Value });
            }
        }
    }

    public bool HasAnyDrumInput(params DrumInputType[] drumInputTypes)
    {
        if (!CheckingInput()) return false;
        for (var i = 0; i < mDrumInputEvents.Count; i++)
        {
            var inputEvent = mDrumInputEvents[i];
            if (inputEvent.Processed) continue;
            
            for (var j = 0; j < drumInputTypes.Length; j++)
            {
                if (inputEvent.Type == drumInputTypes[j])
                {
                    inputEvent.Processed = true;
                    return true;
                }
            }
        }
        return false;
    }

    public bool GetDrumInput(DrumInputType drumInputType)
    {
        if (!CheckingInput()) return false;
        for (var i = 0; i < mDrumInputEvents.Count; i++)
        {
            var inputEvent = mDrumInputEvents[i];
            if (!inputEvent.Processed && inputEvent.Type == drumInputType)
            {
                inputEvent.Processed = true;
                return true;
            }
        }
        return false;
    }
}
