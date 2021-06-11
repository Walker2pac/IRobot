using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TeamAlpha.Source
{
    public enum RotationDirection
    {
        Forward,
        Back
    }
    public class HorizontalCylinderBarrier : MonoBehaviour
    {
        public RotationDirection CurrentRotationDirection;
        public float RotationSpeed;
        public int DamageValue;

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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == DataGameMain.LayerPlayer)
            {
                PlayerController.Current.SetProcessedDamage(DamageValue);
                Destroy(gameObject);
            }
        }
    }
}