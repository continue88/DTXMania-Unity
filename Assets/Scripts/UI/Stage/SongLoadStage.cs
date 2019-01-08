using System.Collections;
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
        yield return new WaitForSeconds(1.0f);

        StageManager.Instance.Open<PlayingStage>();

        Close();
    }
}
