using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamAlpha.Source
{
    public class Part : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == DataGameMain.LayerPlayer) 
            {
                PlayerController.Current.SetHealthUp(DataGameMain.Default.partHealthUp);
                Destroy(gameObject);
            }
        }
    }
}