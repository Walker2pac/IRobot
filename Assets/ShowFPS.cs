using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowFPS : MonoBehaviour
{
    public static float fps;
    [SerializeField] private TMP_Text fpsText;

    void OnGUI()
    {
        fps = 1.0f / Time.deltaTime;
        GUILayout.Label("FPS: " + (int)fps);
    }

    private void Update()
    {
        fpsText.text = "FPS: " + (int)fps;
    }
}
