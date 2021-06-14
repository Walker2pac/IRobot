using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamAlpha.Source
{
    public class LittleElectricFieldBarriers : MonoBehaviour
    {
        [SerializeField] private int damageValue;
        [SerializeField] private List<ElectricPoint> ElectricPoints = new List<ElectricPoint>();

        private void Start()
        {
            for (int i = 0; i < ElectricPoints.Count; i++)
            {
                ElectricPoints[i].SetDamageValue(damageValue);
            }

        }
    }
}