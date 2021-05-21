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
        public Transform vcamLookAt;
        public Transform vcamFollow;
        [SerializeField] private MovingObject movingObject;
        [SerializeField] private GameObject levelMarkPrefab;

        [Header("Level Mark Colors")]
        [SerializeField] private Color levelMarkActiveColor;
        [SerializeField] private Color levelMarkNonActiveColor;

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
        private int currentLevel = 0;
        #endregion

        private int health;
        private List<Image> levelMarks;

        public MovingObject MovingObjectAccessor { get => movingObject; }

        #region Lifecycle
        public void Start()
        {
            PanelProgress.Default.healthBarSlider.maxValue = DataGameMain.Default.playerHealth;
            health = DataGameMain.Default.playerHealth/levels.Count;
            PanelProgress.Default.healthBarSlider.value = health;
            levelMarks = new List<Image>();
            levels[0].Setup(this);
            for (int i = 0; i < levels.Count; i++) 
            {
                GameObject levelMark = Instantiate(levelMarkPrefab);
                levelMarks.Add(levelMark.GetComponentInChildren<Image>());
                RectTransform rectTransform = levelMark.GetComponent<RectTransform>();
                rectTransform.SetParent(PanelProgress.Default.healthBarSlider.transform);
                levelMark.GetComponentInChildren<Text>().text = "" + (i + 1);
                float anchorOffset = (1f / DataGameMain.Default.playerHealth) * (DataGameMain.Default.playerHealth / levels.Count) * (i + 1);
                rectTransform.anchorMin = new Vector2(-0.5f + anchorOffset, 0.25f);
                rectTransform.anchorMax = new Vector2(0.5f + anchorOffset, 0.75f);
                rectTransform.offsetMin = new Vector2(0f, 0f);
                rectTransform.offsetMax = new Vector2(0f, 0f);
            }
            UpdateHealthBar();
        }

        private void FixedUpdate()
        {
            movingObject.ChangeOffsetX(JoystickController.Default.DeltaSlide);
        }
        #endregion

        #region Methods
        public void SendDamagableObject(DamagableObject damagable) 
        {
            levels[currentLevel].ProcessDamagableObject(damagable);
        }
        public void SetProcessedDamage(int damage) 
        {
            health -= damage;
            if (health <= 0) 
            {
                LevelController.Current.CompleteLevel(false);
                return;
            }
            CheckLevelUpdate();
        }

        public void SetHealthUp(int healthUp) 
        {
            health += healthUp;
            if (health > DataGameMain.Default.playerHealth) health = DataGameMain.Default.playerHealth;
            CheckLevelUpdate();
        }

        private void CheckLevelUpdate() 
        {
            int targetLevel = Mathf.Clamp(( health / (DataGameMain.Default.playerHealth / levels.Count ) - 1 ), 0, levels.Count - 1);
            if (targetLevel != currentLevel) 
            {
                Debug.Log(targetLevel);
                levels[currentLevel].Exit();
                currentLevel = targetLevel;
                levels[currentLevel].Setup(this);
            }
            UpdateHealthBar();
        }

        private void UpdateHealthBar() 
        {
            PanelProgress.Default.healthBarSlider.value = health;

            for (int i = 0; i < levels.Count; i++) 
                levelMarks[i].color = i <= currentLevel ? levelMarkActiveColor : levelMarkNonActiveColor;
        }
        #endregion
    }
}