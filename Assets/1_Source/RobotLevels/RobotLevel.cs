using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamAlpha.Source
{
    public abstract class RobotLevel : ScriptableObject
    {
        [SerializeField] protected GameObject robotModel;
        protected PlayerController _playerController;

        public abstract void Setup(PlayerController playerController);
        public abstract void ProcessDamagableObject(DamagableObject damagable);
        public abstract void Exit();
    }
}