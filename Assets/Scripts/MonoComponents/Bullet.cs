using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


namespace TeamAlpha.Source
{
    public class Bullet : DamagableBody
    {
        [SerializeField, Required] private Rigidbody rigidBody;
        [SerializeField] private ParticleSystem explodeParticles;
        private void Start()
        {
            Invoke("DestroySelf", DataGameMain.Default.bulletLifeDuration);
        }

        public void SetSpeed(Vector3 speed) => rigidBody.velocity = speed;

        private void DestroySelf() 
        {
            explodeParticles?.gameObject.transform.SetParent(null);
            explodeParticles?.Play();
            Destroy(explodeParticles.gameObject, explodeParticles.main.duration);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != DataGameMain.LayerPlayer && other.gameObject.layer != DataGameMain.LayerNonCollision) 
            {
                DamagedReaction();
            }
            else
            {
                if (other.GetComponent<MirrorBarrier>())
                {
                    Debug.Log("mirror");
                    NonDamagedReaction();
                }                
            }
        }

        public override void DamagedReaction() => DestroySelf();
        public override void NonDamagedReaction()
        {           
            rigidBody.velocity *= -1.3f;
            Vector3 reflectDirection = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y-1f, rigidBody.velocity.z);
            rigidBody.velocity = reflectDirection;
        }
    }
}