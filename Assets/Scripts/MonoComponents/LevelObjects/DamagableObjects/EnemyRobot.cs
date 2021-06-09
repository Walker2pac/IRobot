using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamAlpha.Source
{
    public class EnemyRobot : DamagableBody
    {
        [SerializeField] private Gun gun;
        [SerializeField] private MovingObject movingObject;

        void Start()
        {
            if (gun != null) 
            {
                LayerDefault.Default.OnPlayStart += 
                    () => gun.StartShoot(1f, movingObject);
            }
        }

        void Update()
        {

        }

        public override void DamagedReaction(){}
        public override void NonDamagedReaction()
        {
            Destroy(gameObject);
        }
    }
}