using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine;

namespace TeamAlpha.Source
{
    public class ThirdLevel : SecondLevel
    {
        [SerializeField, AssetsOnly] protected Shield shieldPrefab;

        protected Shield shield;

        public override void Setup(PlayerController playerController)
        {
            base.Setup(playerController);
            SpawnShield();
        }

        public override void ProcessDamagableObject(DamagableObject damagable)
        {
            
            if (shield.ProcessDamageByShield()) damagable.NonDamagedReaction();
            else base.ProcessDamagableObject(damagable);
        }

        public override void Exit()
        {
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