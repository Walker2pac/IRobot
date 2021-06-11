using DG.Tweening;
using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : DamagableBody
{
    public override void DamagedReaction() 
    {
        SplinePositioner positioner = GetComponent<SplinePositioner>();
        double pos = positioner.position;
        DOTween.To(
            () => positioner.position,
            (double tweenPosition) => positioner.position = tweenPosition,
            pos + 0.2f, 1f).SetTarget(this);
    }

    


    public override void NonDamagedReaction() 
    {
        transform.DOScale(0f, 0.5f)
            .OnComplete(() => Destroy(gameObject));
    }
}
