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
        bool rotated;
        private void Start()
        {
            rotated = true;
        }

        private void Update()
        {
            if (rotated)
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
        }
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (other.gameObject.layer == DataGameMain.LayerPlayer)
            {
                rotated = false;
            }               
        }
    }
}