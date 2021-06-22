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
    }
}
