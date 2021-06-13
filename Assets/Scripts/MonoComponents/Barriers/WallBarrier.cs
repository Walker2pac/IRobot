using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamAlpha.Source
{
    public class WallBarrier : MonoBehaviour
    {
        public int DamageValue;
        public int StrengtheValue;

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
