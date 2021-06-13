using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TeamAlpha.Source
{
    public class Part : MonoBehaviour
    {
        public GameObject Effect;
        //public Transform SpawnEffect;
        private void Update()
        {
            float speedRotete = Random.Range(0.5f, 2.5f);
            transform.Rotate(0, speedRotete, 0);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == DataGameMain.LayerPlayer) 
            {
                PlayerController.Current.SendPart();
                ParticleSystem destroyParts = Instantiate(Effect, other.transform).GetComponent<ParticleSystem>();
                Destroy(gameObject);
            }
        }
    }
}