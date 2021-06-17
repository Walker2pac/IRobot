using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TeamAlpha.Source
{

    public class MirrorBarrier : MonoBehaviour
    {
        [SerializeField] private MirrorReflection mirror;
        [SerializeField] private List<GameObject> partsBarrier = new List<GameObject>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == DataGameMain.LayerPlayer)
            {
                mirror.transform.position = Vector3.zero;

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
}
