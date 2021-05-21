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
    public class PanelWin : MonoBehaviour
    {
        [Required]
        public Panel panel;
        [Required]
        public Button buttonCountinue;

        public void Start()
        {
            buttonCountinue.onClick.AddListener(HandleButtonCountinueClick);
        }
        private void HandleButtonCountinueClick()
        {
            LayerDefault.Default.Restart();
        }
    }
}
