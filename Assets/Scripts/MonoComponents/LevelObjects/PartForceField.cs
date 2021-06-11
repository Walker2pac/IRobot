using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartForceField : MonoBehaviour
{
    [SerializeField] ParticleSystem onShowParticles;

    private RobotDetails detail;
    private int collisions = 0;

    void Start()
    {
        detail = GetComponentInParent<RobotDetails>();
    }

    private void OnParticleCollision(GameObject other)
    {
        onShowParticles.Play();
        Destroy(onShowParticles.gameObject, onShowParticles.main.duration);
        onShowParticles.transform.SetParent(transform.parent);

        //detail.ShowDetail();
        Destroy(other);
    }
}
