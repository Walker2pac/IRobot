using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

namespace TeamAlpha.Source
{
    public class PreAttachMove : MonoBehaviour
    {
        [SerializeField] private SplineFollower splineFollower;
        private void Start()
        {
            splineFollower.followSpeed = FindObjectOfType<PlayerController>().MovingObject.Speed;
        }
    }
}