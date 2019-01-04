using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public Transform UIRoot;

    public static MainScript Instance { get; private set; }

    // Use this for initialization
    void Start ()
    {
        Instance = this;
        DontDestroyOnLoad(this);

        StageManager.Instance.Open<LaunchStage>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        StageManager.Instance.Update();
    }
}
