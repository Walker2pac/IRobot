using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Dreamteck.Splines;

public enum RotationSide
{
    Left,
    Right,
    None
}



public class PlatformBarrier : BarriersInteractingButton
{
    public float RotationSpeed;
    public RotationSide CurrentRotationSide;
    [Range(1, 2)]
    public int NumberBarriers;
    public GameObject BarrierPrefab;
    [Header("Barriers Position On Platform")]
    public List<Transform> BarriersPosition = new List<Transform>();
    

    private void Start()
    {
        if (NumberBarriers == 1)
        {
            Instantiate(BarrierPrefab, BarriersPosition[0]);
        }
        else
        {
            for (int i = 1; i < BarriersPosition.Count; i++)
            {
                Instantiate(BarrierPrefab, BarriersPosition[i]);
            }
        }
    }

    private void FixedUpdate()
    {
        if (CurrentRotationSide == RotationSide.Right)
        {
            transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
        }
        if (CurrentRotationSide == RotationSide.Left)
        {
            transform.Rotate(0, -RotationSpeed * Time.deltaTime, 0);
        }

    }


    public override void Action(float speed)
    {
        transform.DOLocalRotate(new Vector3(0, 90, 0), speed).OnComplete(() => CurrentRotationSide = RotationSide.None);
    }
}
