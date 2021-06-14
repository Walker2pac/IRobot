using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace TeamAlpha.Source
{
    public class BigElectricFieldBarrier : Barriers, IButton
    {
        [SerializeField] private List<ElectricPoint> points = new List<ElectricPoint>();

        private void Start()
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i].SetDamageValue(damageValue);
            }

        }
        public void PushButton(float speed)
        {
            Off();
            /*for (int i = 0; i < points.Count; i++)
            {
                points[i].transform.DOLocalMoveY(transform.localPosition.y, speed, false).OnComplete(() => Off());
            }*/

        }

        void Off()
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].GetComponentInChildren<ParticleSystem>())
                {
                    points[i].GetComponentInChildren<ParticleSystem>().Stop();
                    points[i].GetComponent<Collider>().enabled = false;
                }
            }
        }
    }
}