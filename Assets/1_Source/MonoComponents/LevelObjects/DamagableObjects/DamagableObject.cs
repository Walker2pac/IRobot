using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TeamAlpha.Source
{
    public class DamagableObject : MonoBehaviour
    {
        public enum Type { SoftBarrier, HardBarrier, Ammo }

        public Type objectType;
        [SerializeField, Required] private DamagableBody body;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == DataGameMain.LayerPlayer)
            {
                PlayerController.Current.SendDamagableObject(this);
            }
            else if (other.gameObject.layer == DataGameMain.LayerEnemyRobot)
            {
                other.gameObject.GetComponentInParent<EnemyRobot>().NonDamagedReaction();
            }
        }

        public void DamagedReaction() => body.DamagedReaction();
        public void NonDamagedReaction() => body.NonDamagedReaction();
    }
}