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
        [SerializeField] private AnimationClip _animRunWithShield;
        [SerializeField] private AnimationClip _animTrip;
        [SerializeField] private AnimationClip _animTripWithShield;

        [Space]
        [SerializeField] private float speed;

        private MovingObject _movingObject;
        private DetailController _detailController;

        public MovingObject MovingObject => _movingObject;
        
        #region Lifecycle
        public void Start()
        {
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
                GameOver();
            else 
            {
                if (!Saw.Default.Spawned) 
                {
                    _movingObject.ChangeSpeed(0f, 0f, () => _movingObject.ChangeSpeed(speed, 1f));
                    StartCoroutine(TrippingAnim(shield));
                }
            }
        }

        private IEnumerator TrippingAnim(bool withShield) 
        {
            _animacer.Play(withShield ? _animTripWithShield : _animTrip, withShield ? 0.5f : 0.2f);
            yield return new WaitForSeconds(withShield ? 1.5f : 0.3f);
            _animacer.Play(_animRun, 1f);
        }

        public void SendPart()
        {
            _detailController.AddDetail();
        }

        private void GameOver() 
        {
            _detailController.DeathEffect();
            _movingObject.ChangeSpeed(0f, 0f);
            UIManager.Default.CurState = UIManager.State.Failed;
            _animacer.Stop();
        }
        #endregion
    }
}