using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsbMidiDriver : MonoBehaviour
{
    Dictionary<int, bool> mMidiNoteInput = new Dictionary<int, bool>();

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
        if (segments.Length == 5 && int.TryParse(segments[3], out midiNote))
            return;

        mMidiNoteInput[midiNote] = false;
    }

    void onMidiNoteOn(string noteInfo)
    {
        if (string.IsNullOrEmpty(noteInfo)) return;

        // deviceAddress,cable,channel,note,velocity
        var segments = noteInfo.Split(',');
        var midiNote = 0;
        if (segments.Length == 5 && int.TryParse(segments[3], out midiNote))
            return;

        mMidiNoteInput[midiNote] = true;
    }

    public bool GetMidiNoteOn(int midiNote)
    {
        var inputOn = false;
        return mMidiNoteInput.TryGetValue(midiNote, out inputOn) && inputOn;
    }
}
