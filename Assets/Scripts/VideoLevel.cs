using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
[CreateAssetMenu(fileName = "New Video Level", menuName = "Custom/Video Level")]

public class VideoLevel : Level
{
    public VideoClip HiddenClip;
    public VideoClip PeakingClip;
    public Texture StaticBackground;
    public float RotationToTarget;

    public VideoLevel(Vector3 TargetLocation, OnScreenMessage Message, VideoClip HiddenClip, VideoClip PeakingClip, Texture staticBackground, float rotationToTarget) : base(TargetLocation, Message)
    {
        this.HiddenClip = HiddenClip;
        this.PeakingClip = PeakingClip;
        StaticBackground = staticBackground;
        RotationToTarget = rotationToTarget;
    }
}
