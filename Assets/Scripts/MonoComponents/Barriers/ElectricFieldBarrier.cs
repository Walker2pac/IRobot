using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace TeamAlpha.Source
{
    public class ElectricFieldBarrier : Barriers, IButton
    {
        [SerializeField] private List<GameObject> points = new List<GameObject>();

        public void PushButton(float speed)
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i].transform.DOMoveY(transform.position.y - 0.002f, speed, false).OnComplete(() => Off());
            }
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