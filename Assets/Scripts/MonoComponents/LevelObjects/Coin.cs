using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;


namespace TeamAlpha.Source
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] bool randomTimeRotate;
        [SerializeField] CoinEffect coinEffect;
        [ShowIf ("@randomTimeRotate==false")]
        [SerializeField, Range(3f, 7f)] private float rotationTime;
        private PanelCoin panel;        

        private void Start()
        {
            if (randomTimeRotate)
            {
                rotationTime = Random.Range(4, 7);
            }
            panel = FindObjectOfType<PanelCoin>();
            RotateCoin(false);
        }
        void RotateCoin(bool stoped)
        {
            transform.DORotate(new Vector3(0, 360,0), rotationTime, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(int.MaxValue, LoopType.Incremental);
            if (stoped)
            {
                transform.DORotate(new Vector3(0, 360, 0), rotationTime, RotateMode.Fast).OnComplete(() => transform.DOKill());
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == DataGameMain.LayerPlayer)
            {
                RotateCoin(true);
                float screenPosition = transform.position.x / Screen.width * 50000; 
                CoinEffect coin = Instantiate(coinEffect);
                coin.SetPosition(screenPosition);
                panel.UpdateCoinText(1);
                Destroy(gameObject);
            }
            
        }

    }
}
