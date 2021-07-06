using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamAlpha.Source
{
    public class LevelController : MonoBehaviour
    {
        #region Singleton
        public static LevelController Current => _current;
        private static LevelController _current;

        public void SetAsCurrent()
        {
            if (_current != null){}
            _current = this;
            GetComponentInChildren<PlayerController>().SetAsCurrent();
        }
        #endregion

        #region Lifecycle
        private void Start(){}

        public void Update(){}
        #endregion

        public void CompleteLevel(bool isPlayerWin)
        {
            UIManager.Default.CurState = isPlayerWin ? UIManager.State.Win : UIManager.State.Failed;
            
            PlayerController.Current.MovingObject.ChangeSpeed(0f, DataGameMain.Default.startSpeedChangeDuration);
            LayerDefault.Default.Playing = false;
        }

    }
}