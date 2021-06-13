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

    public class VerticalCylinderBarrier : MonoBehaviour
    {
        public RotationSide CurrentRotationSide;
        public float RotationSpeed;
        public int DamageValue;

        private void Update()
        {
            if(CurrentRotationSide == RotationSide.Right)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * RotationSpeed);
            }
            if (CurrentRotationSide == RotationSide.Left)
            {
                transform.Rotate(-Vector3.up * Time.deltaTime * RotationSpeed);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == DataGameMain.LayerPlayer)
            {
                PlayerController.Current.SendDamage(DamageValue);
                Destroy(gameObject);
            }
        }
    }
}
