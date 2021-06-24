using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartForceField : MonoBehaviour
{
    [SerializeField] ParticleSystem onShowParticles;

    private RobotDetails detail;

    void Start()
    {
        detail = GetComponentInParent<RobotDetails>();
        Debug.Log(detail.name);
        onShowParticles.transform.SetParent(transform.parent);
        onShowParticles.transform.localScale = Vector3.one;
        Invoke("Attach", 0.2f);

    }

    void Attach()
    {
        onShowParticles.Play();
        Destroy(onShowParticles.gameObject, onShowParticles.main.duration);

        detail.StartAttach();
        //Destroy(other);
        Destroy(gameObject);
    }

    /*private void OnParticleCollision(GameObject other)
    {
        
    }*/

}
