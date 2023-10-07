using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Image Level", menuName = "Custom/Image Level")]

public class ImageLevel : Level
{
    public Texture SurroundingTexture;

    public ImageLevel(Vector3 TargetLocation, OnScreenMessage Message, Texture surroundingTexture) : base(TargetLocation, Message)
    {
        this.SurroundingTexture = surroundingTexture;
    }
}
