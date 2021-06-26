using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TeamAlpha.Source
{
    public class BarrierOnPlatform : Barriers
    {

        public void SetDamgeValue(int value)
        {
            base.damageValue = value;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            Vector3 randomVector = Vector3.one * Random.Range(-1, 2);
            Vector3 forceDirection = (gameObject.transform.position - transform.position).normalized;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(3 * forceDirection, ForceMode.Impulse);
            rb.AddTorque(randomVector * 3f, ForceMode.Impulse);

        }

        protected override void Broken()
        {
            Destroy(gameObject);
        }
    }
}
