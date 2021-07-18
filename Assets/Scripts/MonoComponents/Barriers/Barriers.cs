using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace TeamAlpha.Source
{

    public class Barriers : MonoBehaviour
    {
        [Header("Damage")]
        [SerializeField] protected int damageValue;
        [Header("Object Behavior")]
        [SerializeField] private bool breakingObject;
        [ShowIf("breakingObject")]
        [SerializeField] protected int health;
        [ShowIf("breakingObject")]
        [SerializeField] private bool breakingByPlayer;

        [SerializeField] private bool changToDynamics;
        [ShowIf("breakingByPlayer")]
        [SerializeField] private RamCollider ramCollider;
        [ShowIf("changToDynamics")]
        [SerializeField] private GameObject staticObject;
        [ShowIf("changToDynamics")]
        [SerializeField] private GameObject dynamicOject;

        [ShowIf("breakingObject")]
        [SerializeField] private List<GameObject> partsBarrier = new List<GameObject>();

        protected virtual void Start()
        {
            if (breakingObject)
            {
                Instantiate(ramCollider, transform.position - Vector3.forward * 4.5f, Quaternion.identity);
            }
        }
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Bullet>())
            {

                if (breakingObject)
                {
                    health -= 1;
                    if (health <= 0)
                    {
                        if (changToDynamics)
                        {
                            dynamicOject.SetActive(true);
                            staticObject.SetActive(false);
                        }
                        Broken(5);
                    }
                    ColliderDisable();
                }
            }
            if (other.gameObject.layer == DataGameMain.LayerPlayer)
            {
                if (Saw.Default.Spawned)
                {
                    if (changToDynamics)
                    {
                        dynamicOject.SetActive(true);
                        staticObject.SetActive(false);
                    }
                    Broken(12);
                }
                else
                {
                    PlayerController.Current.SendDamage(damageValue);
                    if (breakingByPlayer)
                    {
                        if (changToDynamics)
                        {
                            dynamicOject.SetActive(true);
                            staticObject.SetActive(false);
                            
                            //StartCoroutine(ScaleParts());
                        }
                        Broken(7);
                    }

                }
                ColliderDisable();

            }

        }

        void ColliderDisable()
        {
            if (GetComponent<Collider>())
            {
                Collider collider = GetComponent<Collider>();
                collider.enabled = false;
            }
            if (GetComponentInChildren<Collider>())
            {
                Collider colliderChild = GetComponentInChildren<Collider>();
                colliderChild.enabled = false;
            }
        }

        IEnumerator ScaleParts()
        {
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < partsBarrier.Count; i++)
            {
                partsBarrier[i].transform.DOScale(Vector3.zero * 0.01f, 1f).SetEase(Ease.InExpo);
                Destroy(partsBarrier[i], 3f);
            }

        }
        protected virtual void Broken(float force)
        {
            for (int i = 0; i < partsBarrier.Count; i++)
            {
                Vector3 randomVector = Vector3.one * Random.Range(-1, 2);
                Vector3 forceDirection = (partsBarrier[i].transform.position - transform.position).normalized;
                Rigidbody rb = partsBarrier[i].GetComponent<Rigidbody>();
                rb.AddForce(force * forceDirection, ForceMode.Impulse);
                rb.AddTorque(randomVector * 3f, ForceMode.Impulse);
                if (partsBarrier[i] != null)
                {
                    StartCoroutine(ScaleParts());
                }
            }

        }

    }
}



public interface IButton
{
    void PushButton(float speed);
}


