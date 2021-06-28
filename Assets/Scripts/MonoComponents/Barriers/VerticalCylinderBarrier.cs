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
        [Space, Header("Position")]
        [SerializeField] private RotationSide currentRotationSide;                
        [SerializeField] private float rotationSpeed;
        [Space, Header("Model")]
        [SerializeField] private GameObject bodyBarrier;
        

        private void Update()
        {
            if (bodyBarrier != null)
            {
                if (currentRotationSide == RotationSide.Right)
                {
                    bodyBarrier.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
                }
                if (currentRotationSide == RotationSide.Left)
                {
                    bodyBarrier.transform.Rotate(-Vector3.up * Time.deltaTime * rotationSpeed);
                }
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
