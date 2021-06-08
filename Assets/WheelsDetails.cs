using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WheelsDetails : MonoBehaviour
{
    public List<SkinnedMeshRenderer> DetailsOnRobot = new List<SkinnedMeshRenderer>();
    public List<SkinnedMeshRenderer> VisualDetail = new List<SkinnedMeshRenderer>();

    public Transform UndockingTarget;
    public Transform DockingTarget;
    public Transform DetailPosition;

    public bool DetailUSed;

    private void Start()
    {
        UndockingDetail();
    }



    public void UndockingDetail()
    {
        DetailUSed = false;
        for (int i = 0; i < DetailsOnRobot.Count; i++)
        {
            DetailsOnRobot[i].enabled = false;
        }
        for (int i = 0; i < VisualDetail.Count; i++)
        {
            VisualDetail[i].enabled = true;
        }
        transform.DOJump(UndockingTarget.position, 2, 1, 1, false).OnComplete(() => SetPosition());

    }

    public void DockingDetail()
    {
        transform.DOMove(DockingTarget.position, 0.5f, false).OnComplete(() => DetailOnRobot());
    }


    void DetailOnRobot()
    {
        for (int i = 0; i < DetailsOnRobot.Count; i++)
        {
            DetailsOnRobot[i].enabled = true;
        }
        for (int i = 0; i < VisualDetail.Count; i++)
        {
            VisualDetail[i].enabled = false;
        }
    }

    void SetPosition()
    {
        transform.position = DetailPosition.position;
    }

}