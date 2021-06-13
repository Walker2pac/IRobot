using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TeamAlpha.Source
{
    public enum TypeBarrier
    {
        Destroyed,
        Undestroyded
    }

    public class Barriers : MonoBehaviour
    {
        [SerializeField] private int damageValue;
        [SerializeField] private TypeBarrier currentTypeBarriers;


        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == DataGameMain.LayerPlayer)
            {
                PlayerController.Current.SendDamage(damageValue);
            }
        }
    }

    public interface IButton
    {        
        void PushButton(float speed);
    }
}