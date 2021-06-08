using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace TeamAlpha.Source
{
    public class PanelStartMenu : MonoBehaviour
    {
        [Required]
        public Panel panel;
        [Required]
        public Button buttonStart;

        public void Start()
        {
            buttonStart.onClick.AddListener(HandleButtonStartClick);
        }
        private void HandleButtonStartClick()
        {
            LayerDefault.Default.Playing = true;
            LayerDefault.Default.OnPlayStart();
            UIManager.Default.CurState = UIManager.State.Play;

                                                    
        }
    }
}
