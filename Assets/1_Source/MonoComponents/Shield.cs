using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamAlpha.Source
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private int shieldHealth;
        [SerializeField] private float shieldRecoveryDuration;
        private int currentShieldHealth;

        
        

        public bool IsActive { get; private set; }

        public void Init(Slider shieldBar, Image fill)
        {
            IsActive = true;
            currentShieldHealth = shieldHealth;
        }

        public bool ProcessDamageByShield()
        {
            if (IsActive)
            {
                Debug.Log("ShieldActive");
                currentShieldHealth -= currentShieldHealth + 1;               
                if (currentShieldHealth <= 0)
                {
                    currentShieldHealth = 0;
                    ActiveShield(false);
                }
                return true;
            }
            return false;
        }

        


        private void ActiveShield(bool arg)
        {
            IsActive = arg;
        }

        public void Destroy() 
        {
            Destroy(gameObject);
        }
    }
}