using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TeamAlpha.Source
{
    public class ShowFPS : MonoBehaviour
    {
        public static float fps;
        [SerializeField] private TMP_Text fpsText;

        private void Start()
        {
            if (DataGameMain.Default.ShowIndicator)
            {
                this.enabled = true;
            }
            else
            {
                this.enabled = false;
            }
        }
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
}