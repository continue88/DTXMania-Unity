using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultTextColumn : Activity
{
    Dictionary<DisplayTrackType, Animation> mTrackDrumMap = new Dictionary<DisplayTrackType, Animation>();

    public ResultTextColumn(GameObject gameObject)
        : base(gameObject)
    {
        for (var i = 0; i < Transform.childCount; i++)
        {
            var drumNode = Transform.GetChild(i).GetComponent<Animation>();
            var displayTrackType = DisplayTrackType.Unknown;
            if (!System.Enum.TryParse(drumNode.name, out displayTrackType))
            {
                Debug.LogError("Track not found for name: " + drumNode.name);
                continue;
            }
            mTrackDrumMap[displayTrackType] = drumNode;
            drumNode.gameObject.SetActive(false);
        }
    }

    public void OnHit(DisplayTrackType displayTrackType, JudgmentType judgmentType)
    {
        if (!mTrackDrumMap.ContainsKey(displayTrackType))
            return;

        var animation = mTrackDrumMap[displayTrackType];
        animation.gameObject.SetActive(true);
        if (animation.isPlaying) animation.Stop();
        animation.Play(judgmentType.ToString());
    }
}
