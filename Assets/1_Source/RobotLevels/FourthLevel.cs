using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamAlpha.Source
{
    public class FourthLevel : ThirdLevel
    {
        [SerializeField] private float durabilityDuration;

        private bool isDurabilityActive;

        public override void Setup(PlayerController playerController)
        {
            base.Setup(playerController);
            LayerDefault.Default.StartCoroutine(ActiveDurabilyty());
        }

        public override void ProcessDamagableObject(DamagableObject damagable)
        {
            if (isDurabilityActive)
                damagable.NonDamagedReaction();
            else
                base.ProcessDamagableObject(damagable);
        }

        public override void Exit()
        {
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