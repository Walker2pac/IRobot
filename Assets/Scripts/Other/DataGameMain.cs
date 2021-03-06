using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TeamAlpha.Source
{
    public class DataGameMain : ScriptableObject
    {
        #region SAVE LOAD KEYS
        public static int LayerPlayer;
        private static string LayerPlayerName = "Player";

        public static int LayerEnemyRobot;
        private static string LayerEnemyRobotName = "Enemy Robot";

        public static int LayerNonCollision;
        private static string LayerNonCollisionName = "NonCollision";
        #endregion
        #region RUNTIME DATA
        public static DataGameMain Default => _default;
        private static DataGameMain _default;

        public int Scores { get; set; }

        #endregion
        #region SETTINGS

        [Header("Player settings")]
        public float maxSpeed;
        public float playerStartSpeed;
        public float startSpeedChangeDuration;
        public int playerHealth;

        [Header("Controll settings")]
        [Range(1f, 50f)]
        public float rotationSharpness;
        public float slideSensitivity;
        public float roadWidth;
        public float roadBounds;

        [Header("Enviroment settings")]
        public int partHealthUp;
        public int barrierDamage;
        public float bulletSpeed;
        public float bulletLifeDuration;

        [Header("Detail docking line settings")]
        public int dockingLineBetweenPoints = 0;
        public float dockingLineNoiseStrenght = 0f;
        public float dockingLineNoiseFrequency = 0f;

        [Header("DockingEffects")]
        public GameObject attachEffect;
        public GameObject brokeEffect;
        public GameObject upgradeEffect;

        [Header("FPS Indicator")]
        public bool ShowIndicator;

        #endregion


        public DataGameMain()
        {
            _default = this;
        }

        public void Init()
        {
            LayerPlayer = LayerMask.NameToLayer(LayerPlayerName);
            LayerEnemyRobot = LayerMask.NameToLayer(LayerEnemyRobotName);
            LayerNonCollision = LayerMask.NameToLayer(LayerNonCollisionName);

        }

        
    }
}
