using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TeamAlpha.Source
{
    public enum RotationDirection
    {
        Forward,
        Back
    }
    public class HorizontalCylinderBarrier : Barriers
    {
        [Space, Header("Position")]
        [SerializeField] private RotationDirection currentRotationDirection;
        [SerializeField, Range(0.5f, 3f)] private float turnTime;
        [SerializeField] private GameObject gear;
        protected override void Start()
        {
            base.Start();
            RotateBarrier(false);
        }
        void RotateBarrier(bool stoped)
        {
            float direction = currentRotationDirection == RotationDirection.Forward ? 360 : -360;
            gear.transform.DORotate(new Vector3(direction, 0, 0), turnTime, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(int.MaxValue, LoopType.Incremental);
            if (stoped)
            {
                gear.transform.DORotate(new Vector3(0, direction, 0), turnTime, RotateMode.Fast).OnComplete(() => gear.transform.DOKill());
            }
        }
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == DataGameMain.LayerPlayer || (other.gameObject.GetComponent<Bullet>()))
            {
                RotateBarrier(true);
            }
            base.OnTriggerEnter(other);     
        }
    }
}