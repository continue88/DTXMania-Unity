using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LaunchStage : Stage
{
    Text mTextCount;

    public override void OnOpen()
    {
        base.OnOpen();

        mTextCount = GetComponent<Text>("TextCount");

        StartCoroutine(DelayOpen());
    }

    private static string GetExternalFilesDir()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (var context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        {
            // Get all available external file directories (emulated and sdCards)
            var externalFilesDirectories = context.Call<AndroidJavaObject[]>("getExternalFilesDirs", null);
            AndroidJavaObject emulated = null;
            AndroidJavaObject sdCard = null;
            for (var i = 0; i < externalFilesDirectories.Length; i++)
            {
                var directory = externalFilesDirectories[i];
                using (var environment = new AndroidJavaClass("android.os.Environment"))
                {
                    // Check which one is the emulated and which the sdCard.
                    var isRemovable = environment.CallStatic<bool>("isExternalStorageRemovable", directory);
                    var isEmulated = environment.CallStatic<bool>("isExternalStorageEmulated", directory);
                    if (isEmulated) emulated = directory;
                    else if (isRemovable && isEmulated == false) sdCard = directory;
                }
            }
            // Return the sdCard if available
            if (sdCard != null)
                return sdCard.Call<string>("getAbsolutePath");
            else
                return emulated.Call<string>("getAbsolutePath");
        }
#else
        return "";
#endif
    }

    /// <summary>
    /// parse the song list.
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayOpen()
    {
        yield return new WaitForSeconds(0.2f);

        // get all the music folder.s
        var musicFolders = new List<string>(MainScript.Instance.MusicFolders);
        var externalFolder = GetExternalFilesDir();
        if (!string.IsNullOrEmpty(externalFolder))
        {
            Debug.Log("externalFolder=" + externalFolder);
            musicFolders.Add(externalFolder);
        }

        var totalLoaded = 0;
        foreach (var folder in musicFolders)
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

                    totalLoaded++;
                    mTextCount.text = totalLoaded.ToString();

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

                totalLoaded++;
                mTextCount.text = totalLoaded.ToString();

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
