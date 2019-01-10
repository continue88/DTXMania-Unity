using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindings
{
    public Dictionary<IdKey, DrumInputType> KeyboardToDrum { get; protected set; }
    public Dictionary<IdKey, DrumInputType> MIDItoDrum { get; protected set; }

    public struct IdKey
    {
        public int DeviceId;
        public int Key;
        public IdKey(int deviceId, int key)
        {
            DeviceId = deviceId;
            Key = key;
        }
    }

    public KeyBindings()
    {
        KeyboardToDrum = new Dictionary<IdKey, DrumInputType>()
        {
            { new IdKey( 0, (int) KeyCode.Q ),      DrumInputType.LeftCrash },
            { new IdKey( 0, (int) KeyCode.Return ), DrumInputType.LeftCrash },
            { new IdKey( 0, (int) KeyCode.A ),      DrumInputType.HiHat_Open },
            { new IdKey( 0, (int) KeyCode.Z ),      DrumInputType.HiHat_Close },
            { new IdKey( 0, (int) KeyCode.S ),      DrumInputType.HiHat_Foot },
            { new IdKey( 0, (int) KeyCode.X ),      DrumInputType.Snare },
            { new IdKey( 0, (int) KeyCode.C ),      DrumInputType.Bass },
            { new IdKey( 0, (int) KeyCode.Space ),  DrumInputType.Bass },
            { new IdKey( 0, (int) KeyCode.V ),      DrumInputType.Tom1 },
            { new IdKey( 0, (int) KeyCode.B ),      DrumInputType.Tom2 },
            { new IdKey( 0, (int) KeyCode.N ),      DrumInputType.Tom3 },
            { new IdKey( 0, (int) KeyCode.M ),      DrumInputType.RightCrash },
            { new IdKey( 0, (int) KeyCode.K ),      DrumInputType.Ride },
        };

        MIDItoDrum = new Dictionary<IdKey, DrumInputType>()
        {
			{ new IdKey( 0,  36 ), DrumInputType.Bass },
            { new IdKey( 0,  30 ), DrumInputType.RightCrash },
            { new IdKey( 0,  29 ), DrumInputType.RightCrash },
            { new IdKey( 1,  51 ), DrumInputType.RightCrash },
            { new IdKey( 1,  52 ), DrumInputType.RightCrash },
            { new IdKey( 1,  57 ), DrumInputType.RightCrash },
            { new IdKey( 0,  52 ), DrumInputType.RightCrash },
            { new IdKey( 0,  43 ), DrumInputType.Tom3 },
            { new IdKey( 0,  58 ), DrumInputType.Tom3 },
            { new IdKey( 0,  42 ), DrumInputType.HiHat_Close },
            { new IdKey( 0,  22 ), DrumInputType.HiHat_Close },
            { new IdKey( 0,  26 ), DrumInputType.HiHat_Open },
            { new IdKey( 0,  46 ), DrumInputType.HiHat_Open },
            { new IdKey( 0,  44 ), DrumInputType.HiHat_Foot },
            { new IdKey( 0, 255 ), DrumInputType.HiHat_Control },	// FDK の MidiIn クラスは、FootControl を ノート 255 として扱う。
			{ new IdKey( 0,  48 ), DrumInputType.Tom1 },
            { new IdKey( 0,  50 ), DrumInputType.Tom1 },
            { new IdKey( 0,  49 ), DrumInputType.LeftCrash },
            { new IdKey( 0,  55 ), DrumInputType.LeftCrash },
            { new IdKey( 1,  48 ), DrumInputType.LeftCrash },
            { new IdKey( 1,  49 ), DrumInputType.LeftCrash },
            { new IdKey( 1,  59 ), DrumInputType.LeftCrash },
            { new IdKey( 0,  45 ), DrumInputType.Tom2 },
            { new IdKey( 0,  47 ), DrumInputType.Tom2 },
            { new IdKey( 0,  51 ), DrumInputType.Ride },
            { new IdKey( 0,  59 ), DrumInputType.Ride },
            { new IdKey( 0,  38 ), DrumInputType.Snare },
            { new IdKey( 0,  40 ), DrumInputType.Snare },
            { new IdKey( 0,  37 ), DrumInputType.Snare },
        };
    }
}
