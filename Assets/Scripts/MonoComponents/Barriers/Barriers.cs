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
        [Header ("Damage")]
        [SerializeField] private TypeBarrier currentTypeBarriers;
        [SerializeField] protected int damageValue;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == DataGameMain.LayerPlayer)
            {
                PlayerController.Current.SendDamage(damageValue);
                if(currentTypeBarriers == TypeBarrier.Destroyed)
                {
                    Broken();
                }
            }
        }

        protected virtual void Broken()
        {
            //Destroy(gameObject);
        }
    }

    public interface IButton
    {        
        void PushButton(float speed);
    }

}