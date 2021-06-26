using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using TMPro;

namespace TeamAlpha.Source
{
    public class PanelFailed : MonoBehaviour
    {
        [Required]
        public Panel panel;
        [Required]
        public Button buttonRestart;
        [Required]
        public TextMeshProUGUI textFailReason;

        public void Awake()
        {
            buttonRestart.onClick.AddListener(HandleButtonRestartClick);
            panel.OnPanelShow += HandlePanelShow;
        }

        private void HandlePanelShow()
        {
            //if (LevelController.Current.FailReson == LevelController.FailReason.FinishedNotFirst)
            //{
            //    textFailReason.text = String.Format("You finished {0} :(", (LevelController.Current.PlayerNumberInRace + 1).AsNumber());
            //}
        }

        private void HandleButtonRestartClick()
        {
            
            LayerDefault.Default.Restart();
        }
    }
}
