using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TeamAlpha.Source
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform bulletSpawn;
        [SerializeField] private Transform model;
        [SerializeField, AssetsOnly] private Bullet bulletPrefab;

        private MovingObject movingObject;
        private float shootingInterval;

        public void StartShoot(float interval, MovingObject mo = null) 
        {
            movingObject = mo;
            shootingInterval = interval;
            Invoke("Shoot", shootingInterval);
        }

        private void Shoot() 
        {
            if (LayerDefault.Default.Playing)
            {
                GameObject bullet = Instantiate(bulletPrefab.gameObject);
                bullet.transform.SetParent(LevelController.Current.transform);
                bullet.transform.position = bulletSpawn.position;
                bullet.transform.rotation = transform.rotation;
                float bulletSpeed = DataGameMain.Default.bulletSpeed;
                if (movingObject != null) bulletSpeed += movingObject.Speed;
                bullet.GetComponent<Bullet>().SetSpeed((bulletSpawn.position - model.transform.position).normalized * bulletSpeed);
                Invoke("Shoot", shootingInterval);
            }
        }
    }
}