using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Dreamteck.Splines;


namespace TeamAlpha.Source
{
    public enum RotationSidePlatform
    {
        Left,
        Right,
        None
    }


    public class PlatformBarrier : Barriers, IButton
    {
        [SerializeField] private float rotationSpeed;
        [SerializeField] private RotationSidePlatform currentRotationSide;
        [Range(1, 2)]
        [SerializeField] private int numberBarriers;
        [SerializeField] private BarrierOnPlatform barrierPrefab;
        [Header("Barriers Position On Platform")]
        [SerializeField] private List<Transform> barriersPosition = new List<Transform>();


        private void Start()
        {
            if (numberBarriers == 1)
            {
                barrierPrefab.SetDamgeValue(damageValue);
                Instantiate(barrierPrefab, barriersPosition[0]);
                
            }
            else
            {
                for (int i = 1; i < barriersPosition.Count; i++)
                {
                    barrierPrefab.SetDamgeValue(damageValue);
                    Instantiate(barrierPrefab, barriersPosition[i]);                    
                }
            }
        }

        private void FixedUpdate()
        {
            if (currentRotationSide == RotationSidePlatform.Right)
            {
                transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            }
            if (currentRotationSide == RotationSidePlatform.Left)
            {
                transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
            }

        }
        public void PushButton(float speed)
        {
            transform.DOLocalRotate(new Vector3(0, 90, 0), speed).OnComplete(() => currentRotationSide = RotationSidePlatform.None);
        }

        void MakeDamade(int damageValue)
        {
            
        }
    }
}