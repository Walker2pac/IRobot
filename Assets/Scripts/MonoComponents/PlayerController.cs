using Animancer;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dreamteck.Splines;

namespace TeamAlpha.Source
{
    [RequireComponent(typeof(MovingObject))]
    [RequireComponent(typeof(DetailController))]

    public class PlayerController : MonoBehaviour
    {
        #region Singleton
        public static PlayerController Current => _current;
        private static PlayerController _current;
        public void SetAsCurrent()
        {
            _current = this;
        }
        #endregion

        [Header("Links")]
        public SplineFollower cameraSpline;
        public Transform vcamLookAt;
        public Transform vcamFollow;
        public SplineFollower preAttachPositionSpline;

        [Header("Animation")]
        [SerializeField] private NamedAnimancerComponent _animacer;
        [SerializeField] private List<AnimationClip> _animsIdle = new List<AnimationClip>();
        [SerializeField] private AnimationClip _animRun;
        [SerializeField] private AnimationClip _animJumpOnPlatform;
        [SerializeField] private List<AnimationClip> _animJump = new List<AnimationClip>();
        [SerializeField] private AnimationClip _animRunWithShield;
        [SerializeField] private AnimationClip _animRamWithShield;
        [SerializeField] private AnimationClip _animTrip;
        [SerializeField] private AnimationClip _animTripDoor;
        [SerializeField] private AnimationClip _animDance;
        [SerializeField] private AnimationClip _animTpose;

        [Space]
        [SerializeField] private float speed;
        [SerializeField] private Transform finishRobotPosition;
        private MovingObject _movingObject;
        private DetailController _detailController;

        public DetailController DetailController => _detailController;

        public MovingObject MovingObject => _movingObject;

        [Header("Outline")]
        [SerializeField] private Outline outline;
        private int seriaPart;

        #region Lifecycle
        public void Start()
        {
            int randomIdleAnim = UnityEngine.Random.Range(0, _animsIdle.Count);
            _animacer.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 180);
            _movingObject = GetComponent<MovingObject>();
            _detailController = GetComponent<DetailController>();
            cameraSpline.startPosition = _movingObject.StartPosition;
            preAttachPositionSpline.startPosition = _movingObject.StartPosition;

            Saw.Default.OnSpawned += () => _movingObject.ChangeSpeed(Saw.Default.MoveSpeed, 0f);
            Saw.Default.OnDeleted += () => _movingObject.ChangeSpeed(speed, 0f);
            Shield.Default.OnShieldSpawned += () => _animacer.Play(_animRunWithShield);
            LayerDefault.Default.OnPlayStart += () =>
            {
                _movingObject.ChangeSpeed(speed, 0f);
                _animacer.Play(_animRun, 0.15f);
            };
            for (int i = 0; i < _animsIdle.Count; i++)
            {
                if(i == randomIdleAnim)
                {
                    _animacer.Play(_animsIdle[i], 0.15f);
                }
                
            }
        }

        private void FixedUpdate()
        {
            _movingObject.ChangeOffsetX(JoystickController.Default.DeltaSlide);
            cameraSpline.followSpeed = _movingObject.Speed;
            preAttachPositionSpline.followSpeed = _movingObject.Speed;
        }
        #endregion

        #region Methods
        public void SendDamage(int damage)
        {
            if (damage > 0)
            {
                bool shield = Shield.Default.Spawned;
                if (!_detailController.LoseDetail(damage))
                {
                    GameOver(false);
                }

                else
                {
                    if (!Saw.Default.Spawned)
                    {              
                        _movingObject.ChangeSpeed(0f, 0f, () => _movingObject.ChangeSpeed(speed, 1f));
                        StartCoroutine(TrippingAnim(damage));
                    }
                }
            }
            
        }
        public IEnumerator Ram()
        {
            bool shield = Shield.Default.Spawned;
            if (shield)
            {
                _animacer.Play(_animRamWithShield, 1f).Speed = 5f;
                yield return new WaitForSeconds(0f);
            }
            if (shield)
            {
                yield return new WaitForSeconds(0);
                _animacer.Play(_animRunWithShield, 0.7f);
            }
            else
            {
                yield return new WaitForSeconds(0);
                _animacer.Play(_animRun, 0.7f);
            }
        }

        private IEnumerator TrippingAnim(int damage)
        {
            
            if (damage > 1)
            {                
                _animacer.Play( _animTrip, 0.2f);
                yield return new WaitForSeconds(0.3f);
            }
            _animacer.Play(_animRun, 0.7f);
        }


                
        public void Jump(bool onPlatform, float duration)
        {
            if (onPlatform)
            {
                StartCoroutine(JumpAnimaPlatform(duration));
            }
            else
            {
                StartCoroutine(JumpAnimaBarrier(duration));
            }
            
        }
        private IEnumerator JumpAnimaBarrier(float duration)
        {
            _animacer.Play(_animJump[0]);
            yield return new WaitForSeconds(0.5f);
            _animacer.Play(_animJump[1], 0.5f).Speed = 1 / duration;
            yield return new WaitForSeconds(duration - 0.5f);
            _animacer.Play(_animRun, 0.5f);
        }

        private IEnumerator JumpAnimaPlatform(float duration)
        {
            _animacer.Play(_animJumpOnPlatform).Speed = 1 / duration;
            yield return new WaitForSeconds(duration);
            _animacer.Play(_animRun, 0.5f);            
        }

        public void SendPart()
        {
            seriaPart = 1;
            outline.OutlineWidth = 0f;
            outline.OutlineColor = new Color(1, 1, 1, 1);
            _detailController.AddDetail();
            StartCoroutine(OutlineRoboto(seriaPart));
        }

        IEnumerator OutlineRoboto(int seria)
        {
            seriaPart += seria;
            Tween tweenWidth = DOTween.To(x => outline.OutlineWidth = x, 0, 7.5f, 0.5f);

            yield return new WaitForSeconds(0.3f * seriaPart);
            Tween tweenColor = DOTween.ToAlpha(() => outline.OutlineColor, c => outline.OutlineColor = c, 0, 0.25f);
            seriaPart = 0;
        }

        public void GameOver(bool door)
        {           
            Time.timeScale = 0.5f;
            _movingObject.ChangeSpeed(0f, 0f);
            if (door)
            {
                _animacer.Play(_animTripDoor, 0.2f);
                for (int i = 0; i < 30; i++)
                {                    
                    _detailController.LoseDetail(1);
                    if (!_detailController.LoseDetail(1))
                    {
                        break;
                    }
                }                
            }
            else
            {
                _animacer.Play(_animTrip, 0.2f);
            }            
            StartCoroutine(DeathRobot());

        }
        IEnumerator DeathRobot()
        {
            yield return new WaitForSeconds(0.5f);
            _detailController.DeathEffect();
            UIManager.Default.CurState = UIManager.State.Failed;
        }

        public void Finish()
        {
            _detailController.FinishLevel();
            cameraSpline.gameObject.GetComponent<CameraAnimation>().FinishPosition();            
            _movingObject.ChangeSpeed(0f, 0f);
            _animacer.gameObject.transform.DOMove(finishRobotPosition.position, 0.1f).OnComplete(() => SetFinishPosition());
        }

        void SetFinishPosition()
        {

            FindObjectOfType<GirlController>().Happy();
            _animacer.Play(_animTpose);
            _animacer.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 180);
            Invoke(nameof(TransferDetail),0.2f);
        }
        void TransferDetail()
        {
            _detailController.DettachDetail();
        }
        #endregion
    }
}