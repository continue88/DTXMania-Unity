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
    public bool CheckExteranlFiles = true;

    public static MainScript Instance { get; private set; }

    public MusicTree MusicTree { get; } = new MusicTree();
    public Score PlayingScore { get; set; } = null;
    public Grade CurrentGrade { get; set; } = null;
    public float InterpSpeed { get; private set; } = 1.0f;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start ()
    {
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

    private void LateUpdate()
    {
        InputManager.Instance.Update();
    }
}
