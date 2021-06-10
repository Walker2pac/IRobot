using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TeamAlpha.Source
{    
    public class PushButtonMechanismBarrier : MonoBehaviour
    {
        public float ResponseSpeed;

        public BarriersInteractingButton BarrierPrefab;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == DataGameMain.LayerPlayer)
            {
                transform.DOLocalMove(new Vector3(0, 0.1f, 0), 0.5f, false).OnComplete(()=> BarrierAction(ResponseSpeed));
            }
        }

        void BarrierAction(float speed)
        {
            BarrierPrefab.Action(speed);
        }
    }
}
