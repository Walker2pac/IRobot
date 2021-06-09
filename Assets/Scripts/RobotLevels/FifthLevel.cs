using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamAlpha.Source
{
    public class FifthLevel : RobotLevel
    {
        public override void Setup(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public override void ProcessDamagableObject(DamagableObject damagable)
        {
            //_playerController.SetProcessedDamage(damage);
        }

        public override void Exit(){}
    }
}