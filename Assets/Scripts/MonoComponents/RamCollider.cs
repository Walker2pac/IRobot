using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TeamAlpha.Source
{
    public class RamCollider : MonoBehaviour
    {
         private void OnTriggerEnter(Collider other)
         {
             if (other.gameObject.layer == DataGameMain.LayerPlayer)
             {
                 PlayerController.Current.StartCoroutine("Ram");
             }
         }
    }
}