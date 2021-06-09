using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TeamAlpha.Source
{
    public class SecondLevel : ThirdLevel
    {
        [SerializeField, AssetsOnly] protected Gun gunPrefab;
        [SerializeField] protected float shootingInterval;
        [SerializeField] protected Transform shootingPosition;
        protected PlayerController _playerControllerDetails;
        
 
        private Gun gun;

       

        public override void Setup(PlayerController playerController)
        {
            base.Setup(playerController);
            base.numberOfDamade = 0;
            
            
            _playerController = playerController;

            FindObjectOfType<DetailScript>().Docking();
            SpawnGun();
            
        }

        public override void ProcessDamagableObject(DamagableObject damagable)
        {
            base.ProcessDamagableObject(damagable);
        }

        public override void Exit()
        {
            FindObjectOfType<DetailScript>().Breaking();
            Destroy(gun.gameObject);
        }


        protected void SpawnGun() 
        {
            GameObject gunObject = Instantiate(gunPrefab.gameObject);
            gunObject.transform.SetParent(_playerController.MovingObjectAccessor.model);
            gunObject.transform.localPosition = new Vector3(0.9f,2f,-0.45f);
            gunObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            gun = gunObject.GetComponent<Gun>();
            gun.StartShoot(shootingInterval, _playerController.MovingObjectAccessor);
        }
    }
}