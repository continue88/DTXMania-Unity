using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrumPad : Activity
{
    PlayingStage mPlayingStage;
    Dictionary<DisplayTrackType, Button> mTrackDrumMap = new Dictionary<DisplayTrackType, Button>();

    public DrumPad(PlayingStage playingStage, GameObject gameObject)
        : base(gameObject)
    {

    }

    public override void OnOpen()
    {
        base.OnOpen();

        for (var i = 0; i < Transform.childCount; i++)
        {
            var drumNode = Transform.GetChild(i).GetComponent<Button>();
            var displayTrackType = DisplayTrackType.Unknown;
            if (!System.Enum.TryParse(drumNode.name, out displayTrackType))
            {
                Debug.LogError("Track not found for name: " + drumNode.name);
                continue;
            }
            mTrackDrumMap[displayTrackType] = drumNode;
        }
    }

    public void OnHit(DisplayTrackType displayTrackType)
    {
        if (!mTrackDrumMap.ContainsKey(displayTrackType))
            return;

        var button = mTrackDrumMap[displayTrackType];
        button.onClick.Invoke();
    }
}
