﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TeamAlpha.Source
{

    public class Barriers : MonoBehaviour
    {
        [Header("Damage")]
        [SerializeField] protected int damageValue;
        [Header("Object Behavior")]
        [SerializeField] private bool breakingObject;
        [ShowIf("breakingObject")]
        [SerializeField] private bool breakingByBullet;
        [ShowIf("breakingByBullet")]
        [SerializeField] protected int health;
        [ShowIf("breakingObject")]
        [SerializeField] private List<GameObject> partsBarrier = new List<GameObject>();

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == DataGameMain.LayerPlayer)
            {
                PlayerController.Current.SendDamage(damageValue);
                Collider collider = GetComponentInChildren<Collider>();
                collider.enabled = false;
                if (breakingObject)
                {
                    Broken();
                }

            }
            if (other.gameObject.GetComponent<Bullet>())
            {
                if (breakingByBullet)
                {
                    health -= 1;
                    if (health <= 0)
                    {
                        Collider collider = GetComponentInChildren<Collider>();
                        collider.enabled = false;
                        Broken();
                    }
                }
            }
        }

        protected virtual void Broken()
        {
            for (int i = 0; i < partsBarrier.Count; i++)
            {
                Vector3 randomVector = Vector3.one * Random.Range(-1, 2);
                Vector3 forceDirection = (partsBarrier[i].transform.position - transform.position).normalized;
                Rigidbody rb = partsBarrier[i].GetComponent<Rigidbody>();
                rb.AddForce(3 * forceDirection, ForceMode.Impulse);
                rb.AddTorque(randomVector * 3f, ForceMode.Impulse);
                if (partsBarrier[i] != null)
                {
                    Destroy(partsBarrier[i], 3f);
                }
            }


        }
    }
}


public interface IButton
{
    void PushButton(float speed);
}


