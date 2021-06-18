﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


namespace TeamAlpha.Source
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] bool randomRotate;
        [ShowIf ("@randomRotate==false")]
        [SerializeField, Range(20, 80)] private float rotationSpeed;
        private PanelCoin panel;        

        private void Start()
        {
            panel = FindObjectOfType<PanelCoin>();
        }
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
            if(other.gameObject.layer == DataGameMain.LayerPlayer)
            {
                panel.UpdateCoinText(1);
                Destroy(gameObject);
            }
            
        }

    }
}