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
        onShowParticles.transform.SetParent(transform.parent);
        onShowParticles.transform.localScale = Vector3.one;
    }

    private void OnParticleCollision(GameObject other)
    {
        onShowParticles.Play();
        Destroy(onShowParticles.gameObject, onShowParticles.main.duration);

        detail.StartAttach();
        Destroy(other);
        Destroy(gameObject);
    }
}
