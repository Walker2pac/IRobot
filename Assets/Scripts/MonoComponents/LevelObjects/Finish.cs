using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TeamAlpha.Source
{
    public class Finish : MonoBehaviour
    {
        private Transform _player { get => PlayerController.Current.transform; }
        [SerializeField] float distanceToActive;
        [SerializeField] List<GameObject> lights = new List<GameObject>();

        private void Update()
        {
            float distance = Vector3.Distance(transform.position, _player.position);
            if(distance< distanceToActive)
            {
                for (int i = 0; i < lights.Count; i++)
                {
                    lights[i].transform.DOScale(new Vector3(1f, 1f, 1.75f), 2f);
                }
            }
            else
            {
                for (int i = 0; i < lights.Count; i++)
                {
                    lights[i].transform.DOScale(new Vector3(0.0005f, 0.0005f, 0.0005f), 0f);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == DataGameMain.LayerPlayer)
            {
                
                PlayerController player = PlayerController.Current;
                player.Finish();
            }
                

        }
    }
}