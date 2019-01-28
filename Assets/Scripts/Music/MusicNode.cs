using SSTFormat.v4;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNode : Node
{
    public string MusicPath { get; private set; }
    public Score Score { get; private set; }

    static readonly char[] PathSeperator = "\\/".ToCharArray();

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

        var path = MusicPath.Substring(0, MusicPath.LastIndexOfAny(PathSeperator) + 1);

        Title = Score.Title;
        SubTitle = Score.Artist;

        if (!string.IsNullOrEmpty(Score.PreviewImage))
            PreviewImagePath = path + Score.PreviewImage;

        if (!string.IsNullOrEmpty(Score.PreviewAudio))
            PreviewAudioPath = path + Score.PreviewAudio;

        Difficulty[3] = new DifficultyLabel { Label = "FREE", Level = (float)Score.Difficulty };
    }
}
