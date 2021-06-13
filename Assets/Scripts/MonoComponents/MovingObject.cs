using DG.Tweening;
using Dreamteck.Splines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TeamAlpha.Source
{
    public class MovingObject : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private SplineFollower splineFollower;
        [SerializeField] public Transform model;
        
        private Tween speedChangeTween;

        public double StartPosition => splineFollower.startPosition;
        public float Speed => splineFollower.followSpeed;

        public void ChangeSpeed(float targetSpeed, float duration, Action onComplete = null)
        {
            if (speedChangeTween != null)
                speedChangeTween.Kill();

            speedChangeTween = DOTween.To(
                () => splineFollower.followSpeed,
                (float tweenSpeed) => splineFollower.followSpeed = tweenSpeed,
                targetSpeed, duration)
                .SetTarget(this)
                .OnComplete(() => onComplete?.Invoke());
        }

        public Vector2 ChangeOffsetX(float deltaSlide)
        {
            float roadWidth = DataGameMain.Default.roadWidth - DataGameMain.Default.roadBounds * 2f;
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
            return newOffset;
        }
    }
}