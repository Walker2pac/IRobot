using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


namespace TeamAlpha.Source
{
    public class PanelWin : MonoBehaviour
    {
        [Required]
        public Panel panel;
        [Required]
        public Button buttonCountinue;
        [SerializeField] private Image lightImage;
        [SerializeField] private Image coinImage;
        [SerializeField] private TMP_Text coinTExt;

        [SerializeField] private TMP_Text levelText;
        private int numberCoin = 0;
        private int number = 0;

        private void OnEnable()
        {
            Debug.Log("start win");
            buttonCountinue.onClick.AddListener(HandleButtonCountinueClick);
            int indexLevel = LayerDefault.Default.curLevelIndex + 1;
            levelText.text = "Level " + indexLevel + "Passed!";
            coinTExt.text = null;
            number = 0;
            numberCoin = FindObjectOfType<PanelCoin>().CoinLevel + 1;
            lightImage.transform.DOScale(Vector3.one, 1f);
            coinImage.transform.DOScale(Vector3.one, 1f).OnComplete(() => StartCoroutine(CoinTextFinal()));
            RotateLight();
            Zoom();
            CoinTextFinal();
        }
        void RotateLight()
        {
            lightImage.transform.DORotate(new Vector3(0, 0, 360), 5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(int.MaxValue, LoopType.Incremental);
        }
        void Zoom()
        {
            lightImage.transform.DOScale(Vector3.one * 0.8f, 2f).OnComplete(() => Unzoom());
        }
        void Unzoom()
        {
            lightImage.transform.DOScale(Vector3.one, 2f).OnComplete(() => Zoom());
        }

        IEnumerator CoinTextFinal()
        {
            for (int i = 0; i < numberCoin; i++)
            {
                if (number == i)
                {
                    coinTExt.text = i.ToString();
                }
                yield return new WaitForSeconds(0.1f);
                number++;
            }
        }
    
    private void HandleButtonCountinueClick()
        {
            LayerDefault.Default.Restart();
        }
    }
}
