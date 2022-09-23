﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class FPSChecker : MonoBehaviour
{
    public Text textFPS;
    float deltaTime = 0.0f;
    
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        textFPS.text = text;
    }
}