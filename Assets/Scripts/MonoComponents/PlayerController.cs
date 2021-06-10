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
        [SerializeField] private MovingObject movingObject;

        #region RobotLevels
        [Serializable]
        public class RobotLevelConnector
        {
            public RobotLevel robotLevelObject;
            public void Setup(PlayerController player) => robotLevelObject.Setup(player);
            public void ProcessDamagableObject(DamagableObject damagable) => robotLevelObject.ProcessDamagableObject(damagable);
            public void Exit() => robotLevelObject.Exit();
        }
        [Header("Robot Levels")]
        public List<RobotLevelConnector> levels = new List<RobotLevelConnector>();
        public int currentLevel = 0;

        #endregion

        [Header("Details")]


        private int health;

        public MovingObject MovingObjectAccessor { get => movingObject; }

        public DetailController _detailController;

        public int currentLevelAnim = 1;
        int targetLevel = 0;


        #region Lifecycle
        public void Start()
        {
            currentLevelAnim = 1;
            health = _detailController.RobotDetails.Count;
            levels[0].Setup(this);
            CheckLevelUpdate();
        }

        private void FixedUpdate()
        {
            movingObject.ChangeOffsetX(JoystickController.Default.DeltaSlide);
            cameraSpline.followSpeed = movingObject.Speed;
        }
        #endregion

        #region Methods
        public void SendDamagableObject(DamagableObject damagable)
        {
            levels[currentLevel].ProcessDamagableObject(damagable);
        }
        public void SetProcessedDamage(int damage)
        {
            if (currentLevel > 0)
            {
                Debug.Log("-1");
                levels[currentLevel].Exit();
                currentLevel = currentLevel - 1;

                levels[currentLevel].Setup(this);
                targetLevel = targetLevel - 1;
                if (currentLevelAnim - 1 > 0)
                {
                    currentLevelAnim = currentLevelAnim - 1;
                }
            }
            else
            {
                if (_detailController.AllDetailsFallen == false)
                {
                    _detailController.FallenDetail(damage);
                }
                else
                {
                    health -= damage;
                }
            }

            if (health <= 0)
            {
                LevelController.Current.CompleteLevel(false);
                return;
            }
            CheckLevelUpdate();
        }

        public void SetHealthUp(int healthUp)
        {
            _detailController.AddDetail();
            health += healthUp;


            if (health > DataGameMain.Default.playerHealth) health = DataGameMain.Default.playerHealth;
            CheckLevelUpdate();
        }

        private void CheckLevelUpdate()
        {
            Debug.Log("Level " + currentLevel + ", targetLevel " + targetLevel + ", currentAnimationClip " + currentLevelAnim);

            if (currentLevel == targetLevel)
            {
                targetLevel++;
                if (currentLevel > 0)
                {
                    Upgrade();
                }

            }

            if (_detailController.HandDetailsDone)
            {
                currentLevel = 1;
            }


            if (_detailController.TorsoDetailsDone)
            {
                currentLevel = 2;
            }
            if (_detailController.FootDetailsDone)
            {
                currentLevel = 3;
            }
        }

        void Upgrade()
        {
            levels[currentLevel].Setup(this);
            currentLevelAnim++;
        }
        #endregion
    }
}