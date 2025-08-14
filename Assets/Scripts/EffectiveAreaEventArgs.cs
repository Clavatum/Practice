using System;
using UnityEngine;

public class EffectiveAreaEventArgs : EventArgs
{
    public string AreaName { get; }
    public AudioClip AudioClip { get; }
    public Vector3 TargetScale { get; }


    public EffectiveAreaEventArgs(string areaName, AudioClip audioClip, Vector3 targetScale)
    {
        AreaName = areaName;
        AudioClip = audioClip;
        TargetScale = targetScale;
    }
}