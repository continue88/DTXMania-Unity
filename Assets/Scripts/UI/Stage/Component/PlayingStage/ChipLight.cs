using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipLight : Activity
{
    PlayingStage mPlayingStage;
    Dictionary<DisplayTrackType, Animation> mTrackDrumMap = new Dictionary<DisplayTrackType, Animation>();

    public ChipLight(PlayingStage playingStage, GameObject gameObject)
        : base(gameObject)
    {
        mPlayingStage = playingStage;
    }

    public override void OnOpen()
    {
        base.OnOpen();

        for (var i = 0; i < Transform.childCount; i++)
        {
            var lightAnim = Transform.GetChild(i).GetComponent<Animation>();
            var displayTrackType = DisplayTrackType.Unknown;
            if (!System.Enum.TryParse(lightAnim.name, out displayTrackType))
            {
                Debug.LogError("Track not found for name: " + lightAnim.name);
                continue;
            }
            mTrackDrumMap[displayTrackType] = lightAnim;
            lightAnim.gameObject.SetActive(false); // disable it first.
        }
    }

    public void OnHit(DisplayTrackType displayTrackType)
    {
        if (!mTrackDrumMap.ContainsKey(displayTrackType))
            return;

        var animation = mTrackDrumMap[displayTrackType];
        animation.gameObject.SetActive(true);

        if (animation.isPlaying) animation.Stop();
        animation.Play();
    }
}
