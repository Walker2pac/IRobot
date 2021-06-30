using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace TeamAlpha.Source
{
    public class SpinnerBarrier : Barriers
    {
        [Space, Header("RotationObject")]
        [SerializeField] private GameObject rotationObject;
        [Space, Header("Position")]
        [SerializeField] private RotationSide currentRotationSide;
        [SerializeField, Range(0.5f, 3f)] private float turnTime;

        protected override void Start()
        {
            base.Start();
            float direction = currentRotationSide == RotationSide.Right ? 360 : -360;
            rotationObject.transform.DORotate(new Vector3(0, direction, 0), turnTime, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(int.MaxValue, LoopType.Incremental);
        }
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
        }
    }
}