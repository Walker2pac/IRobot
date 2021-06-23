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

        [Header("Animation")]
        [SerializeField] private NamedAnimancerComponent _animacer;
        [SerializeField] private AnimationClip _animIdle;
        [SerializeField] private AnimationClip _animRun;
        [SerializeField] private AnimationClip _animJumpOnPlatform;
        [SerializeField] private List<AnimationClip> _animJump = new List<AnimationClip>();
        [SerializeField] private AnimationClip _animRunWithShield;
        //[SerializeField] private AnimationClip _animRam;
        [SerializeField] private AnimationClip _animRamWithShield;
        [SerializeField] private AnimationClip _animTrip;
        //[SerializeField] private AnimationClip _animTripWithShield;
        [SerializeField] private AnimationClip _animDance;

        [Space]
        [SerializeField] private float speed;

        private MovingObject _movingObject;
        private DetailController _detailController;

        public MovingObject MovingObject => _movingObject;

        #region Lifecycle
        public void Start()
        {
            _animacer.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 180);
            _movingObject = GetComponent<MovingObject>();
            _detailController = GetComponent<DetailController>();
            cameraSpline.startPosition = _movingObject.StartPosition;

            Saw.Default.OnSpawned += () => _movingObject.ChangeSpeed(Saw.Default.MoveSpeed, 0f);
            Saw.Default.OnDeleted += () => _movingObject.ChangeSpeed(speed, 0f);
            Shield.Default.OnShieldSpawned += () => _animacer.Play(_animRunWithShield);
            LayerDefault.Default.OnPlayStart += () =>
            {
                _movingObject.ChangeSpeed(speed, 0f);
                _animacer.Play(_animRun, 0.15f);
            };
            _animacer.Play(_animIdle, 0.15f);
        }

        private void FixedUpdate()
        {
            _movingObject.ChangeOffsetX(JoystickController.Default.DeltaSlide);
            cameraSpline.followSpeed = _movingObject.Speed;
        }
        #endregion

        #region Methods
        public void SendDamage(int damage)
        {
            bool shield = Shield.Default.Spawned;
            if (!_detailController.LoseDetail(damage))
            {
                Time.timeScale = 0.5f;
                _movingObject.ChangeSpeed(0f, 0f);
                _animacer.Play(_animTrip, 0.2f);
                Invoke("GameOver", 1f);
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
        public IEnumerator Ram()
        {
            bool shield = Shield.Default.Spawned;
            if (shield)
            {
                _animacer.Play(_animRamWithShield, 1f).Speed = 5f;
                yield return new WaitForSeconds(0.7f);
                _animacer.Play(_animRun, 0.3f);
            }
            
        }

        private IEnumerator TrippingAnim(int damage)
        {
            
            if (damage > 1)
            {                
                _animacer.Play( _animTrip, 0.2f);
                yield return new WaitForSeconds(0.3f);
            }
            _animacer.Play(_animRun, 1f);
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
            _detailController.AddDetail();
        }

        private void GameOver()
        {            
            _detailController.DeathEffect();
            Invoke("ShowUI", 1f);
            
        }

        void ShowUI()
        {
            UIManager.Default.CurState = UIManager.State.Failed;
        }

        public void Finish()
        {
            cameraSpline.gameObject.GetComponent<CameraAnimation>().FinishPosition();
            _animacer.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 180);
            bool shield = Shield.Default.Spawned;
            if (shield)
            {
                Shield.Default.Break();
            }
            _movingObject.ChangeSpeed(0f, 0f);
            UIManager.Default.CurState = UIManager.State.Win;
            _animacer.Play(_animDance);
        }
        #endregion
    }
}