using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    public Material Surrounding;
    public VideoPlayer VideoPlayer;

    public GameObject PointingTarget;

    public List<Level> Levels;
    private int CurrentLevelIndex = 0;

    public Canvas OnScreenTextCanvas;
    private TextMeshProUGUI title;
    private TextMeshProUGUI description;

    private bool hidden = false;


    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;

        title = OnScreenTextCanvas.transform.Find("Title").GetComponent<TextMeshProUGUI>();
        description = OnScreenTextCanvas.transform.Find("Description").GetComponent<TextMeshProUGUI>();

        MoveTarget();
        LoadLevelVisual();
        LoadMessage();
    }

    private Level getCurrentLevel()
    {
        return Levels[CurrentLevelIndex];
    }

    public void NextLevel()
    {
        if(CurrentLevelIndex < Levels.Count)
        {
            CurrentLevelIndex++;
            ScreenFadeManager.Instance.Fade();
            MoveTarget();
            if (CurrentLevelIndex >= Levels.Count-1)
            {
                DisableTarget();
            }
        }
        VideoPlayer.enabled = false;
    }

    public void MoveTarget()
    {
        Level current = getCurrentLevel();
        PointingTarget.transform.localPosition = current.TargetLocation;
    }

    void DisableTarget()
    {
        PointingTarget.active = false;
    }

    public void LoadLevelVisual()
    {
        Level current = getCurrentLevel();

        if (current is ImageLevel)
        {
            Surrounding.SetTexture("_MainTex", ((ImageLevel)current).SurroundingTexture);
            VideoPlayer.enabled = false;
        }
        else if (current is VideoLevel)
        {
            VideoPlayer.enabled = true;
            Surrounding.SetTexture("_MainTex", ((VideoLevel)current).StaticBackground);
            VideoPlayer.clip = ((VideoLevel)current).PeakingClip;
            VideoPlayer.Play();
        }
    }

    public void Update()
    {
        float rotationRange = 10;
        if (getCurrentLevel() is VideoLevel)
        {
            VideoLevel current = ((VideoLevel)getCurrentLevel());
            /*if (gameObject.transform.rotation.y >= current.RotationToTarget - rotationRange ||
                gameObject.transform.rotation.y <= current.RotationToTarget + rotationRange &&
                !hidden)
            {
                VideoPlayer.clip = current.HiddenClip;
                VideoPlayer.Play();
                hidden = true;
            }
            else if(
                gameObject.transform.rotation.y < current.RotationToTarget - rotationRange ||
                gameObject.transform.rotation.y > current.RotationToTarget + rotationRange && 
                hidden)
            {
                VideoPlayer.clip = ((VideoLevel)current).PeakingClip;
                VideoPlayer.Play();
                hidden = false;
            }*/
            if(transform.localEulerAngles.y >= (current.RotationToTarget - rotationRange) &&
                transform.localEulerAngles.y <= (current.RotationToTarget + rotationRange) &&
                !hidden)
            {
                //VideoPlayer.Stop();
                VideoPlayer.clip = current.HiddenClip;
                //VideoPlayer.Play();
                Debug.Log("HIDE");
                hidden = true;
            }
            else if((transform.localEulerAngles.y < (current.RotationToTarget - rotationRange) ||
                transform.localEulerAngles.y > (current.RotationToTarget + rotationRange)) && 
                hidden)
            {
                //VideoPlayer.Stop();
                VideoPlayer.clip = current.PeakingClip;
                //VideoPlayer.Play();
                Debug.Log("PEAK");
                hidden = false;
            }
        }
    }

    public void LoadMessage()
    {
        Level current = getCurrentLevel();

        title.text = current.Message.Title;
        description.text = current.Message.Description;
    }
}
