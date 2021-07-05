using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace TeamAlpha.Source
{
    public class GirlController : MonoBehaviour
    {

        [Header("Animation")]
        [SerializeField] private NamedAnimancerComponent _animacer;
        [SerializeField] private AnimationClip _girlTPose;
        [Header("GirlDetail")]
        public List<GameObject> girlDetails = new List<GameObject>();        
        private int _currentDetail;


        private void Start()
        {
            
            _animacer.Play(_girlTPose,0.2f);
            for (int i = 0; i < girlDetails.Count; i++)
            {
                girlDetails[i].SetActive(false);
                
            }
        }

        public void AttachDetail()
        {
            for (int i = 0; i < girlDetails.Count; i++)
            {
                if (i == _currentDetail)
                {
                    girlDetails[i].SetActive(true);
                    
                }
            }
            _currentDetail++;
        }

    }
}
