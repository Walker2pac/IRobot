using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlusOne : MonoBehaviour
{
    [SerializeField] private Image coinPrefab;

    public void SetPosition(float x)
    {
        float indent = Random.Range(-50f, 52f);
        coinPrefab.transform.localPosition = new Vector3(x + indent, -200f, 0f);
        ShowEffect(x);
    }

    void ShowEffect(float x)
    {
        coinPrefab.transform.DOScale(Vector3.one * 0.5f, 0.2f).OnComplete(()=> coinPrefab.transform.DOScale(Vector3.zero, 0.1f)).OnComplete(()=>Destroy(gameObject));
        //coinPrefab.transform.DOLocalMove(new Vector3(x, 0f, 0f), 0.5f).OnComplete(() => coinPrefab.transform.DOLocalMove(new Vector3(x, -30, 0), 0.2f).OnComplete(() => OnBase()));

    }
    void OnBase()
    {
        coinPrefab.transform.DOScale(Vector3.zero, 0.3f);
        
    }
}
