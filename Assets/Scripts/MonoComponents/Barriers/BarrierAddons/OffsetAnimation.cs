using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using DG.Tweening;
using Sirenix.OdinInspector;

public class OffsetAnimation : MonoBehaviour
{
    [SerializeField] private bool advanceSettings;

    [SerializeField, Min(0f), HideIf("advanceSettings")] private float time;
    [SerializeField, Min(0f), HideIf("advanceSettings")] private float delay;

    [SerializeField, Min(0f), ShowIf("advanceSettings")] private float timeAB;
    [SerializeField, Min(0f), ShowIf("advanceSettings")] private float timeBA;
    [SerializeField, Min(0f), ShowIf("advanceSettings")] private float delayA;
    [SerializeField, Min(0f), ShowIf("advanceSettings")] private float delayB;
    [SerializeField, ShowIf("advanceSettings")] private Ease easeAB = Ease.Linear;
    [SerializeField, ShowIf("advanceSettings")] private Ease easeBA = Ease.Linear;

    [Space, SerializeField] private Transform barrierHolder;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    private void Start()
    {
        barrierHolder.localPosition = Vector2.right * barrierHolder.localPosition.x;
        MoveToA();
    }

    private void OnDrawGizmos()
    {
        if (pointA) pointA.localPosition = pointA.localPosition.x * Vector3.right;
        if (pointB) pointB.localPosition = pointB.localPosition.x * Vector3.right;
        if (barrierHolder) barrierHolder.localPosition = barrierHolder.localPosition.x * Vector3.right;
    }

    private void MoveToA() 
    {
        bool a = advanceSettings;
        GenerateTween(a ? timeBA : time, a ? delayA : delay, pointA, easeBA)
            .OnComplete(() => MoveToB());
    }

    private void MoveToB() 
    {
        bool a = advanceSettings;
        GenerateTween(a ? timeAB : time, a ? delayB : delay, pointB, easeAB)
            .OnComplete(() => MoveToA());
    }

    private Tween GenerateTween(float t, float d, Transform point, Ease ease) 
    {
        Tween tween = DOTween.To(
            () => barrierHolder.localPosition.x,
            (v) => barrierHolder.localPosition = Vector2.right * v,
            point.localPosition.x, t)
            .SetEase(ease)
            .SetDelay(d);

        return tween;
    }
}
