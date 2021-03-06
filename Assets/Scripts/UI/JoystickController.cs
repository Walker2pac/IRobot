using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace TeamAlpha.Source
{
    public class JoystickController : MonoBehaviour
    {
        #region Singleton
        public static JoystickController Default => _default;
        private static JoystickController _default;
        public JoystickController() => _default = this;
        #endregion

        [Required]
        public Image stick;
        [Required]
        public GameObject view;


        public float DeltaSlide 
        {
            get 
            {
                float avarage = 0f;
                if (deltas.Count > 0)
                {
                    foreach (float f in deltas)
                        avarage += f;
                    avarage /= deltas.Count;
                }
                return avarage;
            }
            private set 
            {
                deltas.Add(value);
                if (deltas.Count > 5)
                    deltas.RemoveAt(0);
            } 
        }

        private List<float> deltas = new List<float>();
        private float lastPosition;


        public void Start()
        {
            view.gameObject.SetActive(false);
        }
        public void Update()
        {
            if (!LayerDefault.Default.Playing || Input.GetMouseButtonUp(0)) 
            {
                deltas = new List<float>();
                return;
            }


            if (Input.GetMouseButtonDown(0))
                lastPosition = Input.mousePosition.x;
            else if (Input.GetMouseButton(0))
            {
                DeltaSlide = (Input.mousePosition.x - lastPosition) / Screen.width * DataGameMain.Default.slideSensitivity;
                lastPosition = Input.mousePosition.x;
            }
            else 
                deltas = new List<float>();
        }
    }
}
