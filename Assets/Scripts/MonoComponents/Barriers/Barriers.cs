using System.Collections;
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
        [SerializeField] protected int health;
        [ShowIf("breakingObject")]
        [SerializeField] private bool breakingByPlayer;
        [ShowIf("breakingObject")]
        [SerializeField] private bool wall;
        [ShowIf("breakingObject")]
        [SerializeField] private RamCollider ramCollider;
        [ShowIf("wall")]
        [SerializeField] private GameObject staticWall;
        [ShowIf("wall")]
        [SerializeField] private GameObject dynamicWall;

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
                    if (wall)
                    {
                        dynamicWall.SetActive(true);
                        staticWall.SetActive(false);
                    }
                    health -= 1;
                    if (health <= 0)
                    {

                        Collider collider = GetComponentInChildren<Collider>();
                        collider.enabled = false;

                        Broken();
                    }
                }
            }
            if (other.gameObject.layer == DataGameMain.LayerPlayer)
            {
                PlayerController.Current.SendDamage(damageValue);
                Collider collider = GetComponentInChildren<Collider>();
                collider.enabled = false;
                if (breakingByPlayer)
                {
                    if (wall)
                    {
                        dynamicWall.SetActive(true);
                        staticWall.SetActive(false);
                        for (int i = 0; i < partsBarrier.Count; i ++)
                        {
                            if (partsBarrier[i] != null)
                            {
                                Destroy(partsBarrier[i], 3f);
                            }
                        }
                    }
                    else
                    {
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
                
            }
            
        }

    }
}



public interface IButton
{
    void PushButton(float speed);
}


