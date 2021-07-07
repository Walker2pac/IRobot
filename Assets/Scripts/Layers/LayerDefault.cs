using Animancer;
using Cinemachine;
//using LionStudios;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
//using HomaGames.HomaBelly;
using UnityEngine;

namespace TeamAlpha.Source
{

    public class LayerDefault : MonoBehaviour
    {
        [Serializable]
        public class Level
        {
            [OnValueChanged("_EditorOnSkyboxChanged")]
            public Material skybox;
            [Required, AssetsOnly]
            public GameObject prefab;
            public float LevelDistanceTotal => 0f;

            #if UNITY_EDITOR
            private void _EditorOnSkyboxChanged()
            {
                LayerDefault layer = FindObjectOfType<LayerDefault>();
                layer._EditorUpdateCurLevelIndex();
            }
            #endif
        }

        private static string SaveKeyLastLevelIndex = "LastLevelIndex";

        public static LayerDefault Default => _default;
        private static LayerDefault _default;

        [LabelText("Choose level from editor?"), SerializeField]
        private bool _EditorChooseLevel;
        [ShowIf("_EditorChooseLevel"), SerializeField, OnValueChanged("_EditorUpdateCurLevelIndex"),
            InlineButton("_EditorCurLevelIncrease", ">>"),
            InlineButton("_EditorCurLevelDecrease", "<<")]
        public int _EditorCurLevelIndex;

#if UNITY_EDITOR
        private void _EditorCurLevelDecrease()
        {
            _EditorCurLevelIndex--;
            _EditorUpdateCurLevelIndex();
        }
        private void _EditorCurLevelIncrease()
        {
            _EditorCurLevelIndex++;
            _EditorUpdateCurLevelIndex();
        }
        public void _EditorUpdateCurLevelIndex()
        {
            UnityEditor.EditorUtility.SetDirty(gameObject);
            if (_EditorCurLevelIndex > levels.Count)
                _EditorCurLevelIndex = 1;
            else if (_EditorCurLevelIndex < 1)
                _EditorCurLevelIndex = levels.Count;
            if (!Application.isPlaying)
            {
                EnableSelectedLevel(_EditorCurLevelIndex - 1);
                return;
            }
            else
            {
                curLevelIndex = _EditorCurLevelIndex - 1;
                Restart();
            }
        }
        [Sirenix.OdinInspector.Button]
        private void CleanProgress()
        {
            PlayerPrefs.DeleteAll();
        }
#endif

        public List<Level> levels;
        [Required]
        public CinemachineVirtualCamera vcam_main;
        [Required]
        public Transform levelHolder;
        public Level CurLevel => levels[curLevelIndex];
        public event Action OnAnimationGlobalSpeedChanged = () => { };

        public bool Playing
        {
            get => playing;
            set
            {
                if (value) LevelStarted = value;
                playing = value;
            }
        }
        public bool LevelStarted { get; private set; }
        public bool PlayerWon
        {
            get => playerWon;
            set
            {
                //if (value)
                    //HomaBelly.Instance.TrackProgressionEvent(ProgressionStatus.Complete, (curLevelIndex + 1).ToString(), 0);
                playerWon = value;
            }
        }
        public float AnimSpeedGlobal
        {
            get => animSpeedGlobal;
            set
            {
                foreach (AnimancerComponent animancer in animancers)
                    animancer.Playable.Speed = value;
                animSpeedGlobal = value;
                OnAnimationGlobalSpeedChanged();
            }
        }
        private float animSpeedGlobal;
        private bool playerWon;
        private bool playing;
        public int curLevelIndex;
        private List<AnimancerComponent> animancers = new List<AnimancerComponent>();
        private bool firstStartPassed;
        private DataGameMain dataGameMain;
        public LayerDefault() => _default = this;

        public Action OnPlayStart = () => { };

        public void Awake()
        {
            UIManager.Default.CurState = UIManager.State.MainMenu;

            curLevelIndex = PlayerPrefs.GetInt(SaveKeyLastLevelIndex, 0);
#if UNITY_EDITOR
            if (_EditorChooseLevel)
                curLevelIndex = _EditorCurLevelIndex - 1;
#endif
            dataGameMain = Resources.Load<DataGameMain>("DataGameMain");
            dataGameMain.Init();
            Restart();
        }
        public void Restart()
        {
            FindObjectOfType<PanelCoin>().CoinLevel = 0;
            BrokenDetail[] brokenDetail = FindObjectsOfType<BrokenDetail>();
            for (int i = 0; i < brokenDetail.Length; i++)
            {
                Destroy(brokenDetail[i].gameObject);
            }

            Time.timeScale = 1f;
            OnPlayStart = () => { };
            LevelStarted = false;
            if (PlayerWon)
            {
                curLevelIndex++;
                if (curLevelIndex >= levels.Count)
                    curLevelIndex = 0;
                //Analytics.Events.LevelStarted(curLevelIndex + 1);
                //HomaBelly.Instance.TrackProgressionEvent(ProgressionStatus.Start, (curLevelIndex + 1).ToString(), 0);
                PlayerWon = false;
            }
            else if (!firstStartPassed)
                firstStartPassed = true;
            //else
                //HomaBelly.Instance.TrackProgressionEvent(ProgressionStatus.Fail, (curLevelIndex + 1).ToString(), 0);
            
            if (curLevelIndex >= levels.Count)
                curLevelIndex = 0;
            PlayerPrefs.SetInt(SaveKeyLastLevelIndex, curLevelIndex);
            UIManager.Default.CurState = UIManager.State.MainMenu;
            UpdateLevel();
            AnimSpeedGlobal = 1f;
            Debug.Log(curLevelIndex + "level");
        }

        private void UpdateLevel()
        {
            EnableSelectedLevel(curLevelIndex);

            if (Application.isPlaying)
            {
                animancers = new List<AnimancerComponent>(FindObjectsOfType<AnimancerComponent>());
                
            }
        }
        private void EnableSelectedLevel(int levelIndex)
        {
            while (levelHolder.childCount != 0)
            {
                GameObject go = levelHolder.GetChild(0).gameObject;
                go.transform.SetParent(null);
                DestroyImmediate(go);
            }
            Level curLevel = levels[levelIndex];
            RenderSettings.skybox = curLevel.skybox;
#if UNITY_EDITOR
            if (Application.isPlaying)
                Instantiate(curLevel.prefab, levelHolder);
            else
                UnityEditor.PrefabUtility.InstantiatePrefab(curLevel.prefab, levelHolder);
#elif true
                Instantiate(curLevel.prefab, levelHolder);
#endif
            if (Application.isPlaying)
                FindObjectOfType<LevelController>().SetAsCurrent();
            PlayerController playerCtrl = FindObjectOfType<PlayerController>();
            if (playerCtrl != null)
            {
                vcam_main.Follow = playerCtrl.vcamFollow;
                vcam_main.LookAt = playerCtrl.vcamLookAt;
            }
        }
#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Restart();
        }
#endif
    }
}