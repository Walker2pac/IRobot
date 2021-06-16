using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TeamAlpha.Source
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private bool shootOnStart;
        [SerializeField] private float shootingInterval;
        [SerializeField] private Transform bulletSpawn;
        [SerializeField] private Transform model;
        [SerializeField] private MovingObject movingObject;
        [SerializeField] private ParticleSystem shootParticles;
        [SerializeField, AssetsOnly] private Bullet bulletPrefab;

        public Action onShoot = () => { };
        [HideInInspector] public bool isShooting;

        private Coroutine _shootCoroutine;

        private void Start()
        {
            if (shootOnStart) StartShoot();
        }

        private void OnEnable()
        {
            if (isShooting) 
            {
                StopAllCoroutines();
                StartShoot();
            }
        }

        public void StartShoot() 
        {
            _shootCoroutine = StartCoroutine(Shoot());
        }

        private IEnumerator Shoot() 
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                if (LayerDefault.Default.Playing && isShooting)
                {
                    GameObject bullet = Instantiate(bulletPrefab.gameObject);
                    bullet.transform.SetParent(LevelController.Current.transform);
                    bullet.transform.position = bulletSpawn.position;
                    bullet.transform.rotation = model.rotation;
                    float bulletSpeed = DataGameMain.Default.bulletSpeed;
                    if (movingObject != null) bulletSpeed += movingObject.Speed;
                    bullet.GetComponent<Bullet>().SetSpeed(bulletSpawn.forward * bulletSpeed);
                    shootParticles?.Play();
                    onShoot?.Invoke();
                }
                yield return new WaitForSeconds(shootingInterval);
            }
        }
    }
}