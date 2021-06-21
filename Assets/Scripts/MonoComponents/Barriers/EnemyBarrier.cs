using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using Animancer;


namespace TeamAlpha.Source
{

    public class EnemyBarrier : Barriers
    {
        
        [Header("Animation")]
        [SerializeField] private NamedAnimancerComponent _animacer;
        [SerializeField] private AnimationClip _animRun;
        [SerializeField] private AnimationClip _animIdle;
        [Header("Move")]
        [SerializeField] private SplineFollower splineFollower;
        private void Start()
        {
            LayerDefault.Default.OnPlayStart += () =>
            {
                splineFollower.followSpeed = 8f;
                _animacer.Play(_animRun, 0.15f);
            };
            _animacer.Play(_animIdle, 0.15f);
            splineFollower.followSpeed = 0f;
        }
        protected override void OnTriggerEnter(Collider other)
        {            
            base.OnTriggerEnter(other);
            if (base.health<=0)
            {
                splineFollower.followSpeed = 0f;
                _animacer.Stop();
            }                       
        }

    }
}