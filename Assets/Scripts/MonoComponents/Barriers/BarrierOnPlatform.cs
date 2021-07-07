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
            /*if (other.gameObject.layer == DataGameMain.LayerPlayer)
            {
                transform.parent = null;
            }*/
            base.OnTriggerEnter(other);
        }


    }
}
