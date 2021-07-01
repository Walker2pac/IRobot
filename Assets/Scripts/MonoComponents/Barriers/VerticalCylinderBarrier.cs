using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TeamAlpha.Source
{
    public enum RotationSide
    {
        Left,
        Right
    }

    public class VerticalCylinderBarrier : Barriers
    {
        [Space, Header("RotationObject")]
        [SerializeField] private GameObject rotationObject;
        [Space, Header("Position")]
        [SerializeField] private RotationSide currentRotationSide;
        [SerializeField, Range(0.5f, 3f)] private float turnTime;


        protected override void Start()
        {
            base.Start();
            RotateBarrier(false);
        }

        void RotateBarrier(bool stoped)
        {
            float direction = currentRotationSide == RotationSide.Right ? 360 : -360;
            rotationObject.transform.DORotate(new Vector3(0, direction, 0), turnTime, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(int.MaxValue, LoopType.Incremental);
            if (stoped)
            {
                rotationObject.transform.DORotate(new Vector3(0, direction, 0), turnTime, RotateMode.Fast).OnComplete(() => rotationObject.transform.DOKill());
            }
        }
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (other.gameObject.layer == DataGameMain.LayerPlayer || (other.gameObject.GetComponent<Bullet>()))
            {
                RotateBarrier(true);
            }
            
        }
    }
}
