using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TeamAlpha.Source
{
    public class Barrel : Barriers
    {
        [SerializeField] private float minDistance =5;
        [SerializeField] private List<Barrel> neighbourBarrel = new List<Barrel>();

        private void Start()
        {
            Barrel[] allBarrels = FindObjectsOfType<Barrel>();
            for (int i = 0; i < allBarrels.Length; i++)
            {
                float distance = Vector3.Distance(allBarrels[i].transform.position, transform.position);
                if(distance<= minDistance)
                {
                    neighbourBarrel.Add(allBarrels[i]);
                }
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Bullet>())
            {
                for (int i = 0; i < neighbourBarrel.Count; i++)
                {
                    neighbourBarrel[i].Broken(5);
                }
            }
            base.OnTriggerEnter(other);
        }
    }
}