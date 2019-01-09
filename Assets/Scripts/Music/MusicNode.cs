using SSTFormat.v4;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNode : Node
{
    public string MusicPath { get; private set; }
    public Score Score { get; private set; }

    public MusicNode(string musicPath, Node parent, Stream stream = null)
    {
        MusicPath = musicPath;
        Parent = parent;

        LoadSongData(stream);
    }

    private void LoadSongData(Stream stream = null)
    {
        Score = stream != null ?
            Score.LoadFromStream(stream, MusicPath) :
            Score.LoadFromFile(MusicPath);

        var path = Path.GetDirectoryName(MusicPath) + "/";

        Title = Score.Title;
        SubTitle = Score.Artist;

        if (!string.IsNullOrEmpty(Score.PreviewImage))
            PreviewImagePath = path + Score.PreviewImage;

        if (!string.IsNullOrEmpty(Score.PreviewAudio))
            PreviewAudioPath = path + Score.PreviewAudio;
    }

    public void OnLoadPreviewImage(WWW www)
    {
        if (string.IsNullOrEmpty(www.error))
        {
            var loadedTextue = www.textureNonReadable;
            if (loadedTextue)
                PreviewSprite = Sprite.Create(loadedTextue, new Rect(0, 0, loadedTextue.width, loadedTextue.height), new Vector2(0.5f, 0.5f));
            else
                Debug.LogError("The previou image not a valid texture: " + www.url);
        }
        else
        {
            Debug.LogError("Fail to load preview image from: " + www.url);
        }
    }
}
