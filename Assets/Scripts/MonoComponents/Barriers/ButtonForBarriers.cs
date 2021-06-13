using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TeamAlpha.Source
{    
    public class ButtonForBarriers : MonoBehaviour, IButton
    {
        [SerializeField] private float responseSpeed;
        [SerializeField] private Barriers barrierPrefab;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == DataGameMain.LayerPlayer)
            {
                transform.DOLocalMove(new Vector3(0, 0.1f, 0), 0.5f, false).OnComplete(()=> PushButton(responseSpeed));
            }
        }
        public void PushButton(float speed)
        {
            barrierPrefab.GetComponent<IButton>().PushButton(speed);
        }
    }
}
