using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeCanvas : MonoBehaviour
{
    public float FadeSpeed;
    public float FadeTime;

    private CanvasGroup canvasGroup;

    private string previousHeaderText;
    private TextMeshProUGUI title;

    private float fadeSpeedCounter;
    private float fadeTimeCounter;

    private bool fading;
    private bool elementVisible;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        title = transform.Find("Title").GetComponent<TextMeshProUGUI>();

        fading = false;
        elementVisible = false;
    }

    void Update()
    {
        if (title.text != previousHeaderText)
        {
            previousHeaderText = title.text;
            fading = true;
            Reset();
        }

        if(fading && !elementVisible)
        {
            FadeIn();
        } 
        else if(fading && elementVisible)
        {
            fadeTimeCounter += Time.deltaTime;
            if(fading && fadeTimeCounter >= FadeTime)
            {
                FadeOut();
            }
        }
    }

    void FadeIn()
    {
        if(canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += FadeSpeed * Time.deltaTime;
        }
        else
        {
            elementVisible = true;
        }
    }

    void FadeOut()
    {
        if (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= FadeSpeed * Time.deltaTime;
        }
        else
        {
            elementVisible = false;
            fading = false;
            Reset();
        }
    }

    void Reset()
    {
        fadeSpeedCounter = 0;
        fadeTimeCounter = 0;
    }
}
