using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorBarrier : BarriersInteractingButton
{
    public List<Transform> DoorLeaves = new List<Transform>();
    

    public override void Action(float speed)
    {
        for (int i = 0; i < DoorLeaves.Count; i++)
        {
            if (DoorLeaves[i].localPosition.x > 0)
            {
                DoorLeaves[i].DOLocalRotate(new Vector3(0, -90, 0), speed);
            }
            else
            {
                DoorLeaves[i].DOLocalRotate(new Vector3(0, 90, 0), speed);
            }
            
        }
    }
}
