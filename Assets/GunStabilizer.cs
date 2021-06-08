using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStabilizer : MonoBehaviour
{
    [SerializeField] private Transform gunPoint;
    [SerializeField] private float followSpeed;


    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, gunPoint.position, Time.deltaTime * followSpeed);
    }
}
