using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine;

namespace TeamAlpha.Source
{
    public class ThirdLevel : FirstLevel
    {
        [SerializeField, AssetsOnly] protected Shield shieldPrefab;

        protected Shield shield;

        public GameObject ShieldModel;
        protected int numberOfDamade;


        public override void Setup(PlayerController playerController)
        {
            base.Setup(playerController);
            SpawnShield();
            Debug.Log("Щит появился");
            numberOfDamade = 0;
            FindObjectOfType<ShieldModel>().ActiveModeleShield();
        }

        public override void ProcessDamagableObject(DamagableObject damagable)
        {
            if (numberOfDamade < 1)
            {
                if (shield.ProcessDamageByShield())
                {
                    damagable.NonDamagedReaction();
                    FindObjectOfType<ShieldModel>().UnactiveShieldModel();
                    Debug.Log("Щит отвалился");
                    numberOfDamade++;
                }
            }
        }

        public override void Exit()
        {
            FindObjectOfType<ShieldModel>().UnactiveShieldModel();
            shield.Destroy();
            base.Exit();
        }

        protected void SpawnShield()
        {
            shield = Instantiate(shieldPrefab, PlayerController.Current.transform);
            shield.transform.localPosition = Vector3.up;
            shield.Init(PanelProgress.Default.shieldBarSlider, PanelProgress.Default.shieldBar);
        }
    }
}