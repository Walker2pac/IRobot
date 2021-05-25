﻿using DG.Tweening;
using Sirenix.OdinInspector;
using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using PathCreation.Examples;
using UnityEngine;
using Animancer;


namespace TeamAlpha.Source
{
    public class MovingObject : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private SplineFollower splineFollower;
        [SerializeField] public Transform model;

        [Header("Animation")]
        [SerializeField] private NamedAnimancerComponent animancer;
        [SerializeField, ShowIf("animancer"), Required] private AnimationClip animationRun;
        [SerializeField, ShowIf("@animancer != null"), Required] private AnimationClip animationIDLE;
        [SerializeField, ShowIf("@animancer != null"), Required] private AnimationClip animationRunWithShield;
        [SerializeField, ShowIf("@animancer != null"), Required] private AnimationClip animationRunOnWhell;

        [Header("Values")]
        [SerializeField] private float startSpeed;

        public float Speed { get => splineFollower.followSpeed; }

        private Tween speedChangeTween;

        private int _currentRobotLevel;

        #region Lifecycle
        private void Start() 
        {
            LayerDefault.Default.OnPlayStart +=
                () => ChangeSpeed(startSpeed, DataGameMain.Default.startSpeedChangeDuration);
        }

        private void FixedUpdate()
        {
            
            if (animancer != null) 
            {
                if(FindObjectOfType<PlayerController>().currentLevel < 2)
                {
                    if (Speed > 0f) animancer.Play(animationRun).Speed = (Speed / DataGameMain.Default.maxSpeed) * 2f;
                    else animancer.Play(animationIDLE, 0.2f);
                }
                
                if (FindObjectOfType<PlayerController>().currentLevel == 2)
                {
                    animancer.Play(animationRunWithShield, 0.2f).Speed = (Speed / DataGameMain.Default.maxSpeed) * 2f;
                }
                if (FindObjectOfType<PlayerController>().currentLevel == 3)
                {
                    animancer.Play(animationRunOnWhell, 0.2f).Speed = (Speed / DataGameMain.Default.maxSpeed) * 2f;
                }
            }
                
        }
        #endregion



        public void ChangeSpeed(float targetSpeed, float duration) 
        {
            if (speedChangeTween != null)
                speedChangeTween.Kill();

            speedChangeTween = DOTween.To(
                () => splineFollower.followSpeed,
                (float tweenSpeed) => splineFollower.followSpeed = tweenSpeed,
                targetSpeed, duration)
                .SetTarget(this);
        }

        public void ChangeOffsetX(float deltaSlide) 
        {
            float roadWidth = DataGameMain.Default.roadWidth - DataGameMain.Default.roadBounds*2f;
            float deltaOffsetX = deltaSlide * roadWidth;
            Vector2 newOffset = splineFollower.motion.offset + new Vector2(deltaOffsetX, splineFollower.motion.offset.y);
            newOffset.x = Mathf.Clamp(newOffset.x, -(roadWidth / 2f), roadWidth / 2f);

            if (Mathf.Abs(newOffset.x) == roadWidth / 2f) deltaOffsetX = 0f;
            if (splineFollower.followSpeed != 0f)
            {
                float t = 1 / ((splineFollower.followSpeed * Time.fixedDeltaTime) + Mathf.Abs(deltaOffsetX)) * Mathf.Abs(deltaOffsetX);
                float modelRotation = Mathf.Lerp(0f, 90f, t) * Mathf.Sign(deltaOffsetX);
                model.transform.localRotation = Quaternion.Lerp(model.transform.localRotation,
                                                                Quaternion.Euler(0f, modelRotation, 0f),
                                                                Time.fixedDeltaTime * DataGameMain.Default.rotationSharpness);

            }

            splineFollower.motion.offset = newOffset;
        }
    }
}