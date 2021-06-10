using DG.Tweening;
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
        public List<AnimationClip> RunStates = new List<AnimationClip>();
        public int CurrentRunState;

        [Header("Values")]
        [SerializeField] private float startSpeed;
        [SerializeField] private float playerSpeed;
        

        public float Speed { get => splineFollower.followSpeed; }


        private Tween speedChangeTween;


        #region Lifecycle
        private void Start()
        {
            LayerDefault.Default.OnPlayStart +=
                () => ChangeSpeed(startSpeed, DataGameMain.Default.startSpeedChangeDuration);

        }

        private void FixedUpdate()
        {
            CheckRunState();

            if (animancer != null)
            {
                for (int i = 0; i < RunStates.Count; i++)
                {
                    if (Speed > 0)
                    {
                        if (i == CurrentRunState)
                        {
                            if (CurrentRunState == 4)
                            {
                                playerSpeed = playerSpeed * 2;
                            }
                            animancer.Play(RunStates[i]).Speed = (Speed / DataGameMain.Default.maxSpeed) * 2f;
                        }
                    }


                    else animancer.Play(RunStates[0], 0.2f);
                }
            }
        }
        public void CheckRunState()
        {

            CurrentRunState = FindObjectOfType<PlayerController>().currentLevelAnim;


        }
        #endregion



        public void ChangeSpeed(float targetSpeed, float duration)
        {
            /*if (speedChangeTween != null)
                speedChangeTween.Kill();

            speedChangeTween = DOTween.To(
                () => splineFollower.followSpeed,
                (float tweenSpeed) => splineFollower.followSpeed = tweenSpeed,
                targetSpeed, duration)
                .SetTarget(this);*/
            splineFollower.followSpeed = playerSpeed;
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