using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFadeManager : MonoBehaviour
{
    public static ScreenFadeManager Instance { get; private set; }


    public float FadeSpeed;
    public float FadeTime;

    private bool screenDark;
    private bool levelVisualLoaded;
    private bool lowering;
    private float albedo;
    private float fadeTimeCounter;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;

        Reset();
    }

    void Update()
    {
        if(screenDark)
        {
            if(!lowering && albedo <= 2.55)
            {
                albedo += (Time.deltaTime * 2.55f) / FadeSpeed;
            }
            else if(fadeTimeCounter < FadeTime)
            {
                if(!levelVisualLoaded)
                {
                    GameStateManager.Instance.LoadLevelVisual();
                    levelVisualLoaded = true;
                }
                fadeTimeCounter += Time.deltaTime;
            }
            else
            {
                GameStateManager.Instance.LoadMessage();

                lowering = true;
                albedo -= (Time.deltaTime * 2.55f) / FadeSpeed;
                if (albedo < 0)
                {
                    Reset();
                    screenDark = false;
                }
            }
        }
        GetComponent<Renderer>().material.SetColor("_Color", new Color(0, 0, 0, albedo));
    }

    public void Fade()
    {
        screenDark = true;
    }

    void Reset()
    {
        albedo = 0;
        fadeTimeCounter = 0;
        levelVisualLoaded = false;
        screenDark = false;
        lowering = false;
    }
}
