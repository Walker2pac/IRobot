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
            // _playerControllerDetails = FindObjectOfType<PlayerController>();
            //_playerControllerDetails.Details[1].Docking();
        }

        public override void ProcessDamagableObject(DamagableObject damagable)
        {

            //  if (shield.ProcessDamageByShield())
            //  {
            if (numberOfDamade < 1)
            {
                if (shield.ProcessDamageByShield())
                {
                    damagable.NonDamagedReaction();
                    //shield.Destroy();
                    FindObjectOfType<ShieldModel>().ActiveModeleShield();
                    Debug.Log("Щит отвалился");
                    numberOfDamade++;
                }
            }
                
           // }
            //else base.ProcessDamagableObject(damagable);
        }

        public override void Exit()
        {
            // _playerControllerDetails = FindObjectOfType<PlayerController>();
            // _playerControllerDetails.Details[1].Breaking();
            
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