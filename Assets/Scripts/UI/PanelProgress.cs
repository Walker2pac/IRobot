using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using DG.Tweening;
using Animancer;

namespace TeamAlpha.Source
{
    public class PanelProgress : MonoBehaviour
    {
        public static PanelProgress Default => _default;
        private static PanelProgress _default;

        [Required] public Panel panel;
        [Required] public Animator animatorScoresCounter;

        [Required] public Image progressBar;
        [Required] public Slider progressBarSlider;

        [Required] public Image healthBar;
        [Required] public Slider healthBarSlider;

        [Required] public Image shieldBar;
        [Required] public Slider shieldBarSlider;

        [Required] public TextMeshProUGUI textScoresCounter;
        public float scoresAnimTime;

        public const string AnimKeyScoresAdd = "ScoresAdd";
        private int curScores;
        private Tweener tweenerScoresCounter;
        private float skillIndicatorOffset;

        public PanelProgress() => _default = this;

        public void Start()
        {
            shieldBarSlider.gameObject.SetActive(false);
            //Observer.Add(DataGameMain.Default, x => x.Scores, scores => UpdateViewScoreCounter());
        }

        private void UpdateViewScoreCounter()
        {
            animatorScoresCounter.Play(AnimKeyScoresAdd);

            if (tweenerScoresCounter != null)
            {
                tweenerScoresCounter.Kill();
                tweenerScoresCounter = null;
            }
            tweenerScoresCounter = DOTween.To(() => curScores, x => curScores = x, DataGameMain.Default.Scores, scoresAnimTime)
                .OnUpdate(() =>
                {
                    textScoresCounter.text = curScores.ToString();
                });
        }

        public void Update()
        {
            if (!LayerDefault.Default.Playing)
                return;
            //Vector3 playerScreenPos = MonoCamera.Default.cam.WorldToScreenPoint(MonoPlayerController.Current.transform.position);
            //playerScreenPos.z = 0f;

            //float progress =
            //    (MonoBallController.Default.rigidbody.transform.position.z -
            //    LayerDefault.Default.playerCtrlStartFrom.startPosition.z) /
            //    LayerDefault.Default.CurLevel.LevelDistanceTotal;
            //progressBar.fillAmount = progress;
            //progressBarSlider.value = progress;

            //float progress = PlayerController.Current.Health / DataGameMain.Default.playerHealth;
            //healthBar.fillAmount = progress;
            //healthBarSlider.value = progress;
        }
    }
}
