using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public enum DetailStates
{
    None,
    StartDocking,
    RedyToDocking,
    DockingComplete,
    Undocking
}

public class RobotDetails : MonoBehaviour
{
    [Header("Renders")]
    public SkinnedMeshRenderer DetailsOnRobot;
    public SkinnedMeshRenderer VisualDetail;
    [Header("Positions")]
    public Transform UndockingTarget;
    public Transform DockingTarget;
    public Transform DetailPosition;

    public Transform PreDockingPosition;
    [Header("States")]
    public DetailStates DetailStates;
    [Header("Effects")]
    public bool UseEffect;
    public UnityEvent EffectDocking;

    public bool DetailUsed;


    public bool Left;

    private void Start()
    {
        UndockingDetail();

    }

    private void Update()
    {
        if (DetailStates == DetailStates.StartDocking)
        {
            transform.position = Vector3.MoveTowards(transform.position, PreDockingPosition.position, Time.deltaTime * 25f);
            if (transform.position == PreDockingPosition.position)
            {
                DetailStates = DetailStates.RedyToDocking;
            }

        }

        if (DetailStates == DetailStates.RedyToDocking)
        {
            transform.position = Vector3.MoveTowards(transform.position, DockingTarget.position, Time.deltaTime * 3f);
            if (transform.position == DockingTarget.position)
            {
                DetailOnRobot();
            }
        }

    }

    public void UndockingDetail()
    {
        DetailUsed = false;
        DetailsOnRobot.enabled = false;
        VisualDetail.enabled = true;
        transform.DOJump(UndockingTarget.position, 2, 1, 1, false).OnComplete(() => SetPosition());

    }

    public void DockingDetail()
    {
        DetailStates = DetailStates.StartDocking;
    }

    void DetailOnRobot()
    {
        DetailUsed = true;
        DetailStates = DetailStates.DockingComplete;
        if (UseEffect)
        {
            EffectDocking.Invoke();
        }
        DetailsOnRobot.enabled = true;
        VisualDetail.enabled = false;

    }

    void SetPosition()
    {
        transform.position = DetailPosition.position;
    }

}