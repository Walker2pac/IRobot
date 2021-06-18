using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamAlpha.Source
{
    public class Finish : MonoBehaviour
    {
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