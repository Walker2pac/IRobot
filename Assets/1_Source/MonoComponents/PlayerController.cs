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
        //[SerializeField] private GameObject levelMarkPrefab;

        /*[Header("Level Mark Colors")]
        [SerializeField] private Color levelMarkActiveColor;
        [SerializeField] private Color levelMarkNonActiveColor;*/

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
        public int currentLevel = 0; //сделал пабликом
        
        #endregion

        [Header("Details")]
        /*public List<DetailScript> Details = new List<DetailScript>();
        public ParticleSystem BrokeEffect;*/


        private int health;
        //private List<Image> levelMarks;

        public MovingObject MovingObjectAccessor { get => movingObject; }

        public DetailController _detailController;

        bool _takeDamage;
        public int currentLevelAnim = 1; //сделал пабликом
        int targetLevel = 0;

        int numberComplete;
        int numberComplete2;
        int numberComplete3;

        #region Lifecycle
        public void Start()
        {
            currentLevelAnim = 1;
            //PanelProgress.Default.healthBarSlider.maxValue = DataGameMain.Default.playerHealth;
            //health = DataGameMain.Default.playerHealth/levels.Count;
            health = _detailController.RobotDetails.Count;
            //PanelProgress.Default.healthBarSlider.value = health;
            //levelMarks = new List<Image>();
            levels[0].Setup(this);
            /*for (int i = 0; i < levels.Count; i++)
            {
                /GameObject levelMark = Instantiate(levelMarkPrefab);
                levelMarks.Add(levelMark.GetComponentInChildren<Image>());
                RectTransform rectTransform = levelMark.GetComponent<RectTransform>();
                rectTransform.SetParent(PanelProgress.Default.healthBarSlider.transform);
                levelMark.GetComponentInChildren<Text>().text = "" + (i + 1);
                float anchorOffset = (1f / DataGameMain.Default.playerHealth) * (DataGameMain.Default.playerHealth / levels.Count) * (i + 1);
                rectTransform.anchorMin = new Vector2(-0.5f + anchorOffset, 0.25f);
                rectTransform.anchorMax = new Vector2(0.5f + anchorOffset, 0.75f);
                rectTransform.offsetMin = new Vector2(0f, 0f);
                rectTransform.offsetMax = new Vector2(0f, 0f);
            }*/
           //UpdateHealthBar();
            CheckLevelUpdate();
            CheckLevelUpdate();
        }

        private void FixedUpdate()
        {
            movingObject.ChangeOffsetX(JoystickController.Default.DeltaSlide);

            if (_takeDamage && currentLevelAnim - 1 > 0 && targetLevel>1)
            {
                currentLevelAnim -= 1;
                currentLevel = 0;
                _takeDamage = false;
            }
           

        }
        #endregion

        #region Methods
        public void SendDamagableObject(DamagableObject damagable)
        {
            levels[currentLevel].ProcessDamagableObject(damagable);
            _takeDamage = true;
        }
        public void SetProcessedDamage(int damage)
        {
            
            //if (_detailController.AllDetailsFallen == false)

            _detailController.FallenDetail(damage);

            health -= damage; //изменить на 1 удар


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
            /*int level = Mathf.Clamp(health / (26 / levels.Count) - 1, 0, levels.Count);
            Debug.Log(level);            

                if (currentLevel != level)
                {
                    levels[currentLevel].Exit();
                    currentLevel = level;
                    levels[currentLevel].Setup(this);
                    currentLevelAnim++;
                    
                }   */

            // добавить счётчик ударов
            
            if (currentLevel == targetLevel)
            {
                targetLevel++;
                //currentLevelAnim++;
            }

            if (_detailController.HandDetailsDone)
            {
                numberComplete++;
                if (numberComplete <= 1)
                {
                    levels[currentLevel].Exit();
                    currentLevel = 1;
                    levels[currentLevel].Setup(this);
                    currentLevelAnim++;
                }
                
                
                //currentLevelAnim++;
            }
            if (_detailController.TorsoDetailsDone)
            {
                numberComplete2++;
                if (numberComplete2 <= 1)
                {
                    levels[currentLevel].Exit();
                    currentLevel = 2;
                    levels[currentLevel].Setup(this);
                    currentLevelAnim++;
                }
                
                //currentLevelAnim++;
            }
            if (_detailController.FootDetailsDone)
            {
                numberComplete3++;
                if (numberComplete3 <= 1)
                {
                    levels[currentLevel].Exit();
                    currentLevel = 3;
                    levels[currentLevel].Setup(this);
                    currentLevelAnim++;
                }
                
                //currentLevelAnim++;
            }



            //Debug.Log(health);
            //Debug.Log(targetLevel);

            /*if (targetLevel != currentLevel)
            {
                //Debug.Log(targetLevel + "Level");
                if (!_takeDamage)
                {

                    levels[currentLevel].Exit();
                    currentLevel = targetLevel;
                    levels[currentLevel].Setup(this);
                }
                else
                {
                    levels[currentLevel].Exit();
                    currentLevel = targetLevel;
                    levels[currentLevel-1].Setup(this);
                    if (currentLevel <= 0)
                    {
                        currentLevel = 0;
                    }
                    _takeDamage = false;

                }


                //_takeDamage = false;
            }*/


            //UpdateHealthBar();
        }

        /*private void UpdateHealthBar()
        {
            PanelProgress.Default.healthBarSlider.value = health;

            for (int i = 0; i < levels.Count; i++)
                levelMarks[i].color = i <= currentLevel ? levelMarkActiveColor : levelMarkNonActiveColor;
        }*/
        #endregion
    }
}