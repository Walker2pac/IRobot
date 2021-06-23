using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace TeamAlpha.Source
{
    public class PanelCoin : MonoBehaviour
    {
        [SerializeField] private TMP_Text coinText;
        private static int allCoin;
        
       

        private void Start()
        {
            coinText.text = allCoin.ToString();
        }

        public void UpdateCoinText(int value)
        {
            allCoin += value;
            Debug.Log(allCoin);
            coinText.text = allCoin.ToString();
        }
    }
}