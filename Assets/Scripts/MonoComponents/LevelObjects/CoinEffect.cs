using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace TeamAlpha.Source
{
    public class CoinEffect : MonoBehaviour
    {
        [SerializeField] private Image coinPrefab;

        public void SetPosition(float x)
        {
            coinPrefab.transform.localPosition = new Vector3(x, -200f, 0);
            ShowEffect(x);
        }

        void ShowEffect(float x)
        {
            coinPrefab.transform.DOScale(Vector3.one, 1f);
            coinPrefab.transform.DOLocalMove(new Vector3(x, 0, 0), 0.5f).OnComplete(() => coinPrefab.transform.DOLocalMove(new Vector3(x, -30, 0), 0.2f).OnComplete(() => OnBase()));

        }
        void OnBase()
        {
            coinPrefab.transform.DOScale(Vector3.zero, 0.3f);
            coinPrefab.transform.DOLocalJump(new Vector3(310, 710, 0), 50, 1, 0.5f, false).OnComplete(() => Destroy(gameObject));
        }
    }
}