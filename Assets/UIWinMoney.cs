using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

namespace TeamAlpha.Source {
    public class UIWinMoney : MonoBehaviour
    {
        [SerializeField] private Image lightImage;
        [SerializeField] private Image coinImage;
        [SerializeField] private TMP_Text coinTExt;

        [SerializeField] private TMP_Text levelText;
        private int numberCoin = 0;
        private int number = 0;


        private void Start()
        {
            // number = 0;
            // coinTExt.text = null;
            int indexLevel = LayerDefault.Default.curLevelIndex + 1;
            levelText.text = "Level " + indexLevel + "Passed!";
            numberCoin = FindObjectOfType<PanelCoin>().CoinLevel + 1;
            lightImage.transform.DOScale(Vector3.one, 1f);
            coinImage.transform.DOScale(Vector3.one, 1f).OnComplete(() => StartCoroutine(CoinTextFinal()));
            RotateLight();
            Zoom();
            CoinTextFinal();
            Debug.Log("money " + numberCoin);
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
    } 
}
