using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TeamAlpha.Source
{
    public class SecondLevel : FirstLevel
    {
        [SerializeField, AssetsOnly] protected Gun gunPrefab;
        [SerializeField] protected float shootingInterval;
        [SerializeField] protected Transform shootingPosition;
        

        private Gun gun;

        public override void Setup(PlayerController playerController)
        {
            _playerController = playerController;
            SpawnGun();
        }

        public override void ProcessDamagableObject(DamagableObject damagable)
        {
            if (damagable.objectType == DamagableObject.Type.SoftBarrier)
            {
                damagable.NonDamagedReaction();
                return;
            }
            else base.ProcessDamagableObject(damagable);
        }

        public override void Exit()
        {
            Destroy(gun.gameObject);
        }


        protected void SpawnGun() 
        {
            GameObject gunObject = Instantiate(gunPrefab.gameObject);
            gunObject.transform.SetParent(_playerController.MovingObjectAccessor.model);
            gunObject.transform.localPosition = Vector3.one / 2;
            gunObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            gun = gunObject.GetComponent<Gun>();
            gun.StartShoot(shootingInterval, _playerController.MovingObjectAccessor);
        }
    }
}