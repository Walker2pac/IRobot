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
        [SerializeField] private RotationDirection currentRotationDirection;
        [SerializeField] private GameObject rotatableObject;
        [SerializeField] private float rotationSpeed;

        private void Update()
        {
            if (currentRotationDirection == RotationDirection.Forward)
            {
                rotatableObject.transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
            }
            if (currentRotationDirection == RotationDirection.Back)
            {
                rotatableObject.transform.Rotate(-Vector3.forward * Time.deltaTime * rotationSpeed);
            }
        }
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == DataGameMain.LayerPlayer)
            {
                rotationSpeed = 0f;
            }
                base.OnTriggerEnter(other);
        }
    }
}