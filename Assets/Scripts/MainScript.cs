using SSTFormat.v4;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public Transform UIRoot;
    public DrumSound DrumSound;
    public WAVManager WAVManager;
    public UsbMidiDriver UsbMidiDriver;
    public string[] MusicFolders;
    public string[] DtxFiles;

    public static MainScript Instance { get; private set; }

    public MusicTree MusicTree { get; } = new MusicTree();
    public Score PlayingScore { get; set; } = null;
    public float InterpSpeed { get; private set; } = 1.0f;

    // Use this for initialization
    void Start ()
    {
        Instance = this;
        DontDestroyOnLoad(this);

        if (!DrumSound) DrumSound = GetComponent<DrumSound>();
        if (!WAVManager) WAVManager = GetComponent<WAVManager>();

        StageManager.Instance.Open<LaunchStage>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        StageManager.Instance.Update();
        SwitchManager.Instance.Update();
    }
}
