using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Pointer : MonoBehaviour
{
    private float counter;
    public int nextLevelAt = 2;
    private bool pointerOnTarget = false;

    void Start()
    {
        counter = 0;
    }

    void Update()
    {
        if(pointerOnTarget)
        {
            counter += Time.deltaTime;
            if (counter >= nextLevelAt)
            {
                counter = 0;
                GameStateManager.Instance.NextLevel();
            }
        }
    }

    public void OnPointEnter()
    {
        pointerOnTarget = true;
    }

    public void onPointLeave()
    {
        pointerOnTarget = false;
        counter = 0;
    }
}
