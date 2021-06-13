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

        private MovingObject _movingObject;
        private DetailController _detailController;

        public MovingObject MovingObject => _movingObject;
        
        #region Lifecycle
        public void Start()
        {
            _movingObject = GetComponent<MovingObject>();
            _detailController = GetComponent<DetailController>();
            cameraSpline.startPosition = _movingObject.SplineFollower.startPosition;
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
            if (!_detailController.LoseDetail(damage))
                GameOver();
        }

        public void SendPart()
        {
            _detailController.AddDetail();
        }

        private void GameOver() 
        {

        }
        #endregion
    }
}