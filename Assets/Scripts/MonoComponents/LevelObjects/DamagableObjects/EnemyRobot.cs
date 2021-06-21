using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamAlpha.Source
{
    public class EnemyRobot : MonoBehaviour
    {
        [SerializeField] private Gun gun;
        // [SerializeField] private MovingObject movingObject;

        [Header("Animation")]
        [SerializeField] private NamedAnimancerComponent _animacer;
        [SerializeField] private AnimationClip _animRun;

        private void Start()
        {
            _animacer.Play(_animRun, 0.15f);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == DataGameMain.LayerPlayer || other.gameObject.GetComponent<Bullet>())
            {
                _animacer.Stop();
            }
        }
        /* void Start()
         {
             if (gun != null) 
             {
                 LayerDefault.Default.OnPlayStart += 
                     () => gun.StartShoot(1f, movingObject);
             }
         }

         void Update()
         {

         }*/


       /* public override void DamagedReaction(){}
        public override void NonDamagedReaction()
        {
            Destroy(gameObject);
        }*/
    }
}