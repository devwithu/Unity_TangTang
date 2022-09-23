using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CavasPixelToUIKitSize : MonoBehaviour
{
    private CanvasScaler _canvasScaler;

    private void Awake()
    {
        _canvasScaler = GetComponent<CanvasScaler>();
        Resize();
    }

    private void Resize()
    {
        if (Application.isEditor)
        {
            return;
        }

        _canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
        _canvasScaler.scaleFactor = Screen.dpi / 160;
        Debug.Log(_canvasScaler.scaleFactor);
    }
}
