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
                if (objectType == Type.HardBarrier)
                {
                    PlayerController.Current.SendDamagableObject(this);
                    Debug.Log("HardBarrier");
                }
                if (objectType == Type.SoftBarrier)
                {
                    PlayerController.Current.SendDamagableObject(this);
                    Debug.Log("SoftBarrier");
                }
                if (objectType == Type.Ammo)
                {                    
                    PlayerController.Current.SendDamagableObject(this);
                    Debug.Log("Ammo");
                }
            }
           /* else if (other.gameObject.layer == DataGameMain.LayerEnemyRobot)
            {
                other.gameObject.GetComponentInParent<EnemyRobot>().DamagedReaction();
            }*/
        }

        public void DamagedReaction() => body.DamagedReaction();
        public void NonDamagedReaction() => body.NonDamagedReaction();
    }
}