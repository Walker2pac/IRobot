using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


namespace TeamAlpha.Source
{
    public class PanelCoin : MonoBehaviour
    {
        [SerializeField] private TMP_Text coinText;
        [SerializeField] private GameObject coinIcon;
        private static int allCoin;
        public int AllCoin => allCoin;

        public int CoinLevel;



        private void Start()
        {
            
            coinText.text = allCoin.ToString();
        }

        public void UpdateCoinText(int value)
        {
            CoinLevel++;
            StartCoroutine(UpdateCoin(value));

        }

        public IEnumerator UpdateCoin(int value)
        {
            yield return new WaitForSeconds(1f);
            allCoin += value;
            coinIcon.transform.DOScale(Vector3.one * 1.1f, 0.2f).OnComplete(() => coinIcon.transform.DOScale(Vector3.one, 0.2f));
            Debug.Log(CoinLevel);
            coinText.text = allCoin.ToString();
        }
    }
}