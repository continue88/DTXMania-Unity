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
            if (!Directory.Exists(folder)) continue;

            foreach (var ext in MusicTree.SearchExtensions)
            {
                yield return new WaitForEndOfFrame();
                foreach (var file in Directory.GetFiles(folder, "*" + ext, SearchOption.AllDirectories))
                {
                    yield return new WaitForEndOfFrame();

                    var musicNode = MainScript.Instance.MusicTree.LoadMusicNode(file);
                    if (musicNode == null) continue;

                    // try to delay load the preview image.
                    if (!string.IsNullOrEmpty(musicNode.PreviewImagePath))
                    {
                        using (var www = new WWW(musicNode.PreviewImagePath))
                        {
                            yield return www;
                            musicNode.OnLoadPreviewImage(www);
                        }
                    }
                }
            }
        }

        // load dtx files in the streaming asset path.
        var streamPath = Application.streamingAssetsPath;
        foreach (var musicFile in MainScript.Instance.DtxFiles)
        {
            var dtxPath = streamPath + musicFile;
            using (var www = new WWW(dtxPath))
            {
                yield return www;

                var musicNode = MainScript.Instance.MusicTree.LoadMusicNode(www);
                if (musicNode == null) continue;

                // try to delay load the preview image.
                if (!string.IsNullOrEmpty(musicNode.PreviewImagePath))
                {
                    using (var www1 = new WWW(musicNode.PreviewImagePath))
                    {
                        yield return www1;
                        musicNode.OnLoadPreviewImage(www1);
                    }
                }
            }
        }

        StageManager.Instance.Open<TitleStage>();

        Close();
    }
}
