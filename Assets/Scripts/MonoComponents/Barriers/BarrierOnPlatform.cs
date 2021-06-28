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

        }

        
    }
}
