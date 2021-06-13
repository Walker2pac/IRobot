using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamAlpha.Source
{
    public enum RotationSide
    {
        Left,
        Right
    }

    public class VerticalCylinderBarrier : Barriers
    {
        [SerializeField] private GameObject bodyBarrier;
        [SerializeField] private RotationSide currentRotationSide;
        [SerializeField] private float rotationSpeed;

        private void Update()
        {
            if(currentRotationSide == RotationSide.Right)
            {
                bodyBarrier.transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
            }
            if (currentRotationSide == RotationSide.Left)
            {
                bodyBarrier.transform.Rotate(-Vector3.forward * Time.deltaTime * rotationSpeed);
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
        }
    }
}
