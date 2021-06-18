using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamAlpha.Source
{
    public class Finish : MonoBehaviour
    {
        private Transform _player { get => PlayerController.Current.transform; }
        [SerializeField] float distanceToActive;
        [SerializeField] List<Lantern> lanterns = new List<Lantern>();

        private void Update()
        {
            float distance = Vector3.Distance(transform.position, _player.position);
            if(distance< distanceToActive)
            {
                for (int i = 0; i < lanterns.Count; i++)
                {
                    lanterns[i].gameObject.SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < lanterns.Count; i++)
                {
                    lanterns[i].gameObject.SetActive(false);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == DataGameMain.LayerPlayer)
            {
                LevelController.Current.CompleteLevel(true);
                PlayerController player = PlayerController.Current;
                player.Finish();
            }
                

        }
    }
}