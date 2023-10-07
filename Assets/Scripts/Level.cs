using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Level : ScriptableObject
{
    public Vector3 TargetLocation;
    public OnScreenMessage Message;

    public Level (Vector3 targetLocation, OnScreenMessage message)
    {
        this.TargetLocation = targetLocation;
        this.Message = message;
    }
}
