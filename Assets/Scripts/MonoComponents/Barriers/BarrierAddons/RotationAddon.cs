using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAddon : MonoBehaviour
{
    enum RotDirection { left, right }

    [SerializeField] RotDirection direction;
    [SerializeField] float speed;
    [SerializeField] float radius = 6f;

    [Space, SerializeField] Transform cylinder;

    private void OnDrawGizmosSelected()
    {
        if (cylinder)
        {
            cylinder.transform.localScale = new Vector3(radius, cylinder.transform.localScale.y, radius);
        }
    }

    private void FixedUpdate()
    {
        float rotation = speed * (direction == RotDirection.left ? 1 : -1);
        cylinder.localRotation *= Quaternion.Euler(Vector3.up * rotation);
    }
}
