using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace TeamAlpha.Source
{
    public class TrampolinePlatform : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "Model")
            {
                other.GetComponentInParent<MovingObject>().OnGround();
            }
        }
    }
}
