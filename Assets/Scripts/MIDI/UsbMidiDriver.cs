using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsbMidiDriver : MonoBehaviour
{
    Dictionary<int, MidiInputStatus> mMidiNoteInput = new Dictionary<int, MidiInputStatus>();

    public System.Action<string> OnMidiNoteOn;

    class MidiInputStatus
    {
        public bool PreviousOn;
        public bool CurrentOn;
    }

    private void LateUpdate()
    {
        foreach (var status in mMidiNoteInput.Values)
            status.PreviousOn = status.CurrentOn;
    }

    void onDeviceAttached(string deviceName) { }
    void onDeviceDetached(string deviceName) { }
    void onMidiInputDeviceAttached(string deviceAddress) { }
    void onMidiInputDeviceDetached(string deviceAddress) { }
    void onMidiOutputDeviceAttached(string deviceAddress) { }
    void onMidiOutputDeviceDetached(string deviceAddress) { }

    void onMidiNoteOff(string noteInfo)
    {
        if (string.IsNullOrEmpty(noteInfo)) return;

        // deviceAddress,cable,channel,note,velocity
        var segments = noteInfo.Split(',');
        var midiNote = 0;
        if (!int.TryParse(segments[segments.Length - 2], out midiNote))
            return;

        OnMidiNoteChanged(midiNote, false);
    }

    void onMidiNoteOn(string noteInfo)
    {
        OnMidiNoteOn?.Invoke(noteInfo);

        if (string.IsNullOrEmpty(noteInfo)) return;

        // deviceAddress,cable,channel,note,velocity
        var segments = noteInfo.Split(',');
        var midiNote = 0;
        if (!int.TryParse(segments[segments.Length - 2], out midiNote))
            return;

        OnMidiNoteChanged(midiNote, true);
    }

    void OnMidiNoteChanged(int midiNote, bool onOff)
    {
        MidiInputStatus inputStatus;
        if (!mMidiNoteInput.TryGetValue(midiNote, out inputStatus))
            mMidiNoteInput[midiNote] = inputStatus = new MidiInputStatus();
        mMidiNoteInput[midiNote].CurrentOn = onOff;
    }

    public bool GetMidiNoteOn(int midiNote)
    {
        MidiInputStatus inputStatus;
        return mMidiNoteInput.TryGetValue(midiNote, out inputStatus) && 
            !inputStatus.PreviousOn &&
            inputStatus.CurrentOn;
    }
}
