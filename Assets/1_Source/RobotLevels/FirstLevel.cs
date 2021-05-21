using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamAlpha.Source
{
    public class FirstLevel : RobotLevel
    {
        public override void Setup(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public override void ProcessDamagableObject(DamagableObject damagable)
        {
            _playerController.SetProcessedDamage(DataGameMain.Default.barrierDamage);
            damagable.DamagedReaction();
        }

        public override void Exit()
        {
   
        }
    }
}