using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace TeamAlpha.Source
{
    public class DoorBarrier : Barriers, IButton
    {
        [SerializeField] private GameObject cylinderTop;

        public void PushButton(float speed)
        {
            cylinderTop.transform.DOLocalRotate(new Vector3(90, 0, 0), speed);
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == DataGameMain.LayerPlayer)
            {
                if (Saw.Default.Spawned)
                {
                    base.OnTriggerEnter(other);
                }
                else
                {
                    Time.timeScale = 0.7f;
                    PlayerController.Current.GameOver(true);
                }
            }
        }
    }
}
