using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LaunchStage : Stage
{
    public override void OnOpen()
    {
        base.OnOpen();

        StartCoroutine(DelayOpen());
    }

    IEnumerator DelayOpen()
    {
        yield return new WaitForSeconds(0.2f);

        foreach (var folder in MainScript.Instance.MusicFolders)
        {
            foreach (var ext in MusicTree.SearchExtensions)
            {
                yield return new WaitForEndOfFrame();
                foreach (var file in Directory.GetFiles(folder, "*" + ext, SearchOption.AllDirectories))
                {
                    yield return new WaitForEndOfFrame();
                    Debug.Log("loading file: " + file);
                    MainScript.Instance.MusicTree.AddMusicNode(file);
                }
            }
        }

        StageManager.Instance.Open<TitleStage>();

        Close();
    }
}
