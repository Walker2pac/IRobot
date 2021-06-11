using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ElectricFieldBarrier : BarriersInteractingButton
{    
    public List<GameObject> Points = new List<GameObject>();

    public override void Action(float speed)
    {
        for (int i = 0; i < Points.Count; i++)
        {
            Points[i].transform.DOMoveY(transform.position.y - 0.002f, speed, false).OnComplete(() => Off());
        }
    }

    void Off()
    {
        for (int i = 0; i < Points.Count; i++)
        {
            if (Points[i].GetComponentInChildren<ParticleSystem>())
            {
                Points[i].GetComponentInChildren<ParticleSystem>().Stop();
                Points[i].GetComponent<Collider>().enabled = false;
            }
        }
    }
}
