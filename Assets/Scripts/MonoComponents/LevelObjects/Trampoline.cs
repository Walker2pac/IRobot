using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


namespace TeamAlpha.Source
{
    public class Trampoline : MonoBehaviour
    {
        [SerializeField] Transform platform;
        [SerializeField] bool jumpOnPlatform;
        [ShowIf("@!jumpOnPlatform"), SerializeField, Range(1,4)] float jumpHeight;
        [ShowIf("@!jumpOnPlatform"), SerializeField, Range(1f,5f)] float jumpDuration;
        private float _jumpHeight;
        private float _jumpDuration;

        private void Start()
        {
            if (jumpOnPlatform)
            {
                _jumpHeight = platform.position.y;
                _jumpDuration = 0.7f;
            }
            else
            {
                _jumpHeight = jumpHeight;
                _jumpDuration = jumpDuration;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "Model")
            {
                Debug.Log("spline");
                other.GetComponentInParent<MovingObject>().Jump(_jumpHeight, _jumpDuration, jumpOnPlatform);
            }
        }
    }
}