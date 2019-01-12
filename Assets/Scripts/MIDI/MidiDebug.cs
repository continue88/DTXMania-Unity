using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MidiDebug : MonoBehaviour
{
    Transform mTransform;
    Text mDebugText;

	// Use this for initialization
	void Start ()
    {
        mTransform = transform;
        mDebugText = GetComponent<Text>();
        MainScript.Instance.UsbMidiDriver.OnMidiNoteOn = ShowDebugText;
    }
	
	// Update is called once per frame
	void Update ()
    {
        mTransform.SetAsLastSibling();
	}

    void ShowDebugText(string text)
    {
        mDebugText.text = text;
    }
}
