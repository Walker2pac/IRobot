using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace TeamAlpha.Source
{
    public class Part : MonoBehaviour
    {
        public GameObject Effect;
        [SerializeField] bool randomTimeRotate;
        [SerializeField] PlusOne plusOneEffectUI;
        [ShowIf("@randomTimeRotate==false")]
        [SerializeField, Range(3, 7)] private float rotationTime;

        private void Start()
        {
            if (randomTimeRotate)
            {
                rotationTime = Random.Range(4, 7);
            }
            RotatePart(false);
        }
        void RotatePart(bool stoped)
        {
            transform.DORotate(new Vector3(0, 360, 0), rotationTime, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(int.MaxValue, LoopType.Incremental);
            if (stoped)
            {
                transform.DORotate(new Vector3(0, 360, 0), rotationTime, RotateMode.Fast).OnComplete(() => transform.DOKill());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            RotatePart(true);
            if (other.gameObject.layer == DataGameMain.LayerPlayer) 
            {
                if (!Saw.Default.Spawned) 
                {
                    PlayerController.Current.SendPart();
                    float screenPositionX = transform.position.x / Screen.width * 50000;
                    PlusOne plusOne = Instantiate(plusOneEffectUI);
                    plusOne.SetPosition(screenPositionX);
                    Destroy(gameObject);
                }
                
            }
        }
    }
}