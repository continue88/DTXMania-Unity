using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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

    private static List<string> GetExternalFilesDir()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (var context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        {
            // Get all available external file directories (emulated and sdCards)
            var externalFilesDirectories = context.Call<AndroidJavaObject[]>("getExternalFilesDirs", null);
            var externDirs = new List<string>();
            for (var i = 0; i < externalFilesDirectories.Length; i++)
            {
                var directory = externalFilesDirectories[i];
                externDirs.Add(directory.Call<string>("getAbsolutePath"));
            }
            return externDirs;
        }
#else
        return null;
#endif
    }

    /// <summary>
    /// parse the song list.
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayOpen()
    {
        // wait for next frame, show the correct UI scene?
        yield return new WaitForEndOfFrame();

        // get all the music folder. (add external storage)
        var musicFolders = new List<string>(MainScript.Instance.MusicFolders);
        var externalDirs = GetExternalFilesDir();
        if (externalDirs != null && externalDirs.Count > 0)
            musicFolders.AddRange(externalDirs);

        // search the folders.
        var loadedFiles = 0;
        if (musicFolders.Count > 0)
        {
            var thread = new Thread(obj =>
            {
                var dirs = obj as List<string>;
                var musicTree = MainScript.Instance.MusicTree;
                foreach (var dir in dirs)
                    musicTree.SearchAndAddToParentNode(musicTree.Root, dir, file => loadedFiles++);
            });
            thread.Start(musicFolders);
            while (thread.IsAlive)
            {
                mTextCount.text = loadedFiles.ToString();
                yield return new WaitForEndOfFrame();
            }
        }

        // load the build-in dtx files in streamingAssetsPath.
        foreach (var dtxFile in MainScript.Instance.DtxFiles)
        {
            using (var www = new WWW(Application.streamingAssetsPath + dtxFile))
            {
                yield return www;

                var musicNode = MainScript.Instance.MusicTree.LoadMusicNode(www);
                if (musicNode == null) continue;

                loadedFiles++;
                mTextCount.text = loadedFiles.ToString();
            }
        }

        // load all the preview images.
        var nodeQueue = new Queue<Node>();
        var previewLoaded = 0;
        nodeQueue.Enqueue(MainScript.Instance.MusicTree.Root);
        while (nodeQueue.Count > 0)
        {
            var node = nodeQueue.Dequeue();
            for (var i = 0; i < node.ChildNodeList.Count; i++)
            {
                var childNode = node.ChildNodeList[i];
                if (childNode.ChildNodeList.Count > 0)
                    nodeQueue.Enqueue(childNode);

                var preImagePath = childNode.PreviewImagePath;
                if (string.IsNullOrEmpty(preImagePath) ||
                    childNode.PreviewSprite) continue;

                if (preImagePath.StartsWith("/")) preImagePath = "file://" + preImagePath;
                using (var www = new WWW(preImagePath))
                {
                    yield return www;
                    childNode.OnLoadPreviewImage(www);
                }

                previewLoaded++;
                mTextCount.text = previewLoaded.ToString();
            }
        }

        StageManager.Instance.Open<TitleStage>();

        Close();
    }
}
