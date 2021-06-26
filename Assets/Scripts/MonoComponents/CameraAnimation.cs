using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamAlpha.Source;
using DG.Tweening;


public class CameraAnimation : MonoBehaviour
{
    [SerializeField] private Transform lookAt;
    [SerializeField] private Transform follow;

    [SerializeField] private float transitionTime;

    [Header("Play position")]
    [SerializeField] private Vector3 lookAtPlay;
    [SerializeField] private Vector3 followPlay;

    [Header("Finish position")]
    [SerializeField] private Vector3 lookAtFinish;
    [SerializeField] private Vector3 followFinish;

    void Start()
    {
        LayerDefault.Default.OnPlayStart += GoToPlayPosition;
    }

    private void GoToPlayPosition() 
    {
        lookAt.DOLocalMove(lookAtPlay, transitionTime);
        follow.DOLocalMove(followPlay, transitionTime);
    }

    public void FinishPosition()
    {
        lookAt.DOLocalMove(lookAtFinish, transitionTime);
        follow.DOLocalMove(followFinish, transitionTime);
    }
    
}
