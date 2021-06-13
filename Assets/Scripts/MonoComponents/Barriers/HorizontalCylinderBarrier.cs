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
        [SerializeField] private RotationDirection CurrentRotationDirection;
        [SerializeField] private float RotationSpeed;

        private void Update()
        {
            if (CurrentRotationDirection == RotationDirection.Forward)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * RotationSpeed);
            }
            if (CurrentRotationDirection == RotationDirection.Back)
            {
                transform.Rotate(-Vector3.up * Time.deltaTime * RotationSpeed);
            }
        }
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
        }
    }
}