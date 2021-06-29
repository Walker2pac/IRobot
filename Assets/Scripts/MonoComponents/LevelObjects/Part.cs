using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace TeamAlpha.Source
{
    public class Part : MonoBehaviour
    {
        public GameObject Effect;
        //[SerializeField] GameObject plusOneEffect;
        [SerializeField] bool randomRotate;
        [SerializeField] PlusOne plusOneEffectUI;
        [ShowIf("@randomRotate==false")]
        [SerializeField, Range(20, 80)] private float rotationSpeed;

        private void Update()
        {
            if (randomRotate)
            {
                float random = Random.Range(20, 60);
                transform.Rotate(new Vector3(0, random * Time.deltaTime, 0));
            }
            else
            {
                transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == DataGameMain.LayerPlayer) 
            {
                if (!Saw.Default.Spawned) 
                {
                    PlayerController.Current.SendPart();
                    float screenPositionX = transform.position.x / Screen.width * 50000;
                    PlusOne plusOne = Instantiate(plusOneEffectUI);
                    plusOne.SetPosition(screenPositionX);
                    /*GameObject effect = Instantiate(plusOneEffect, new Vector3(transform.position.x,transform.position.y,other.transform.position.z), Quaternion.identity);
                    effect.GetComponent<ParticleSystem>().Play();*/

                    //ParticleSystem destroyParts = Instantiate(Effect, other.transform).GetComponent<ParticleSystem>();
                    //PlayerController.Current.DetailController.Outline();
                    Destroy(gameObject);
                }
                
            }
        }
    }
}