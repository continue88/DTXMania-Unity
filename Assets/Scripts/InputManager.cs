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

    public bool HasMoveUp() { return Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0; }
    public bool HasMoveDown() { return Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0; }
    public bool HasMoveRight() { return Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") > 0; }
    public bool HasMoveLeft() { return Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") < 0; }
    public bool HasOk() { return Input.GetButtonDown("Submit") || (Input.touchSupported && Input.touchCount > 0); }
    public bool HasCancle() { return Input.GetButtonDown("Cancel"); }
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

    public bool GetChipInput(DrumInputType drumInputType)
    {
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
