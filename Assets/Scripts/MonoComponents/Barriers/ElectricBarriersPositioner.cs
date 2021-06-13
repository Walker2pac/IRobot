using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamAlpha.Source
{
    public enum SidePosition
    {
        Left,
        Right
    }
    public class ElectricBarriersPositioner : MonoBehaviour
    {
        public List<ElectricPoint> ElectricPoints = new List<ElectricPoint>();
        public SidePosition CurrentSidePosition;

        private void Start()
        {
            if(CurrentSidePosition == SidePosition.Left)
            {
                for (int i = 0; i < ElectricPoints.Count; i++)
                {
                    ElectricPoints[i].gameObject.transform.localPosition = new Vector3(
                        -ElectricPoints[i].gameObject.transform.localPosition.x,
                        ElectricPoints[i].gameObject.transform.localPosition.y,
                        ElectricPoints[i].gameObject.transform.localPosition.z);
                }
            }
        }
    }
}