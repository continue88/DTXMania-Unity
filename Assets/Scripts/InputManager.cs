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

    public bool EnableDrumKeyChecking { get; set; } = false;
    public IReadOnlyList<DrumInputEvent> DrumInputEvents => mDrumInputEvents;
    public IReadOnlyDictionary<KeyBindings.IdKey, DrumInputType> KeyboardToDrum => mKeyBindings.KeyboardToDrum;
    public IReadOnlyDictionary<KeyBindings.IdKey, DrumInputType> MIDItoDrum => mKeyBindings.MIDItoDrum;

    public class DrumInputEvent
    {
        public int DeviceID;
        public int Key;
        public DrumInputType Type;
        public bool Processed = false;

        private DrumInputEvent() { }
        private static Stack<DrumInputEvent> msPool = new Stack<DrumInputEvent>();
        public static DrumInputEvent New(int deviceId, int key, DrumInputType type)
        {
            var instance = msPool.Count > 0 ?
                msPool.Pop() :
                new DrumInputEvent();
            instance.DeviceID = deviceId;
            instance.Key = key;
            instance.Type = type;
            instance.Processed = false;
            return instance;
        }
        public static void Delete(DrumInputEvent instance)
        {
            msPool.Push(instance);
        }
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

    public void Update()
    {
        PollAllInputDevices();
    }

    void PollAllInputDevices()
    {
        if (mDrumInputEvents.Count > 0)
        {
            foreach (var item in mDrumInputEvents)
                DrumInputEvent.Delete(item);
            mDrumInputEvents.Clear();
        }

        // check for the keyboard messages.
        if (EnableDrumKeyChecking)
        {
            foreach (var idKey in mKeyBindings.KeyboardToDrum)
            {
                if (Input.GetKeyDown((KeyCode)idKey.Key.Key))
                    mDrumInputEvents.Add(DrumInputEvent.New(idKey.Key.DeviceId, idKey.Key.Key, idKey.Value));
            }
        }

        // check for the midi input device.
        var midiDriver = MainScript.Instance.UsbMidiDriver;
        if (midiDriver)
        {
            foreach (var idKey in mKeyBindings.MIDItoDrum)
            {
                if (idKey.Key.DeviceId == 0 && midiDriver.GetMidiNoteOn(idKey.Key.Key))
                    mDrumInputEvents.Add(DrumInputEvent.New(idKey.Key.DeviceId, idKey.Key.Key, idKey.Value));
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

    public void EnqueueDrumInputEvent(DrumInputEvent drumInputEvent)
    {
        mDrumInputEvents.Add(drumInputEvent);
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
