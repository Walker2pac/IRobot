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
        [SerializeField] private List<GameObject> partsBarrier = new List<GameObject>();

        private void Update()
        {
            if(currentRotationSide == RotationSide.Right)
            {
                bodyBarrier.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
            }
            if (currentRotationSide == RotationSide.Left)
            {
                bodyBarrier.transform.Rotate(-Vector3.up * Time.deltaTime * rotationSpeed);
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {

            base.OnTriggerEnter(other);
            
            
        }

        protected override void Broken()
        {
            for (int i = 0; i < partsBarrier.Count; i++)
            {
                Vector3 randomVector = Vector3.one * Random.Range(-1, 2);
                Vector3 forceDirection = (partsBarrier[i].transform.position - transform.position).normalized;
                Rigidbody rb = partsBarrier[i].GetComponent<Rigidbody>();
                rb.AddForce(3 * forceDirection, ForceMode.Impulse);
                rb.AddTorque(randomVector * 3f, ForceMode.Impulse);
                Destroy(partsBarrier[i], 5f);
            }
            base.Broken();
        }
    }
}
