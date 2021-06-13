using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamAlpha.Source
{
    
    public class ElectricPoint : MonoBehaviour
    {
        public int DamageValue;

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