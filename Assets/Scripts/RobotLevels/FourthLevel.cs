using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamAlpha.Source
{
    public class FourthLevel : SecondLevel
    {
        [SerializeField] private float durabilityDuration;

        private bool isDurabilityActive;

        public override void Setup(PlayerController playerController)
        {
            base.Setup(playerController);
            FindObjectOfType<WheelModel>().ActiveModeleWheel();
        }

        public override void ProcessDamagableObject(DamagableObject damagable)
        {
            if (isDurabilityActive)
                damagable.NonDamagedReaction();
            else
                base.ProcessDamagableObject(damagable);
            FindObjectOfType<WheelModel>().UnactiveWheelModel();
        }

        public override void Exit()
        {
            FindObjectOfType<WheelModel>().UnactiveWheelModel();
            base.Exit();
        }

        private IEnumerator ActiveDurabilyty() 
        {
            isDurabilityActive = true;
            Renderer rend = shield.GetComponentInChildren<Renderer>();
            Color baseColor = rend.material.GetColor("_Color");
            rend.material.SetColor("_Color", Color.red);
            yield return new WaitForSeconds(durabilityDuration);
            rend.material.SetColor("_Color", baseColor);
            isDurabilityActive = false;
        } 
    }
}