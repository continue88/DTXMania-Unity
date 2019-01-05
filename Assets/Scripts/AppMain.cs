using SSTFormat.v4;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppMain
{
    public static AppMain Instance { get; private set; } = new AppMain();
    private AppMain() { }

    public Score PlayingScore { get; private set; }
    public double InterpSpeed { get; private set; } = 1.0;
}
