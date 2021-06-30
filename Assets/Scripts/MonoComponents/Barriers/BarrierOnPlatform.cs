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
            if (other.gameObject.layer == DataGameMain.LayerPlayer)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.one * 3, ForceMode.Impulse);
                transform.parent = null;
            }
        }


    }
}
