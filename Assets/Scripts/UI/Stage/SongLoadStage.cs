﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongLoadStage : Stage
{
    public override void OnOpen()
    {
        base.OnOpen();

        // setup song preview image.
        var previewImage = GetComponent<Image>("PreviewImage");
        var previewSprite = MainScript.Instance.MusicTree.FocusNode.PreviewSprite;
        if (previewImage && previewSprite)
            previewImage.sprite = previewSprite;

        StartCoroutine(DelayOpen());
    }

    IEnumerator DelayOpen()
    {
        while (SwitchManager.Instance.CurrentSwitch != null)
            yield return null;

        MainScript.Instance.WAVManager.Clear();

        var playingScore = MainScript.Instance.PlayingScore;
        foreach (var kvp in playingScore.WAVList)
        {
            var path = playingScore.PATH_WAV + '/' + kvp.Value.Item1;

            using (var www = new WWW(path))
            {
                yield return www;
                MainScript.Instance.WAVManager.Sinup(kvp.Key, www, kvp.Value.Item2);
            }
        }

        Resources.UnloadUnusedAssets();

        yield return new WaitForSeconds(1.0f);

        StageManager.Instance.Open<PlayingStage>();
        
        Close();
    }
}
