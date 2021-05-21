using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionHandler : MonoBehaviour
{
    private Camera camera;

    private void Awake()
    {
        camera = FindObjectOfType<Camera>();
        SetFieldOfView();
    }

    private void SetFieldOfView()
    {
        float screenRatio = (1.0f * Screen.height) / (1.0f * Screen.width);
        if (1.7f < screenRatio && screenRatio < 1.8f)
        {
            camera.fieldOfView = 60;
        }
        if (2.1f < screenRatio && screenRatio < 2.2f)
        {
            camera.fieldOfView = 75;
        }
    }
}
