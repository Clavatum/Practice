using System;
using UnityEngine;

public class EffectiveAreaEventArgs : EventArgs
{
    public string AreaName { get; }
    public AudioClip AudioClip { get; }

    public EffectiveAreaEventArgs(string areaName, AudioClip audioClip)
    {
        AreaName = areaName;
        AudioClip = audioClip;
    }
}