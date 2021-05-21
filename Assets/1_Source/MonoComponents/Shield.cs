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
        [Space]
        [SerializeField] private Color shieldBarColorActive;
        [SerializeField] private Color shieldBarColorNonActive;

        private Tween shieldRecovery;
        private Slider shieldBar;
        private Image shieldBarFillArea;

        private int currentShieldHealth;

        public bool IsActive { get; private set; }

        public void Init(Slider shieldBar, Image fill)
        {
            IsActive = true;
            shieldBar.gameObject.SetActive(true);
            shieldBarFillArea = fill;
            this.shieldBar = shieldBar;
            shieldBar.maxValue = shieldHealth;
            shieldBar.value = shieldHealth;
            currentShieldHealth = shieldHealth;
        }

        public bool ProcessDamageByShield()
        {
            if (IsActive)
            {
                currentShieldHealth -= DataGameMain.Default.barrierDamage;
                shieldBar.value = currentShieldHealth;
                if (currentShieldHealth <= 0)
                {
                    currentShieldHealth = 0;
                    ActiveShield(false);

                    if (shieldRecovery != null)
                        shieldRecovery.Kill();
                    shieldRecovery = DOTween.To(
                        () => currentShieldHealth,
                        (float tweenHealth) =>
                            {
                                currentShieldHealth = (int)tweenHealth;
                                shieldBar.value = tweenHealth;
                            },
                        shieldHealth, shieldRecoveryDuration)
                        .SetTarget(this)
                        .OnComplete(() => ActiveShield(true));
                }
                return true;
            }
            return false;
        }


        private void ActiveShield(bool arg)
        {
            IsActive = arg;
            shieldBarFillArea.color = arg ? shieldBarColorActive : shieldBarColorNonActive;
        }

        public void Destroy() 
        {
            shieldBar.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}