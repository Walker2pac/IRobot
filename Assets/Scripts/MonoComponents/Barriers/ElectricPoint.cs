using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamAlpha.Source
{
    
    public class ElectricPoint : Barriers
    {
        public void SetDamageValue(int value)
        {
            base.damageValue = value;
        }
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
        }
    }
}