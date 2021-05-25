using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DetailScript : MonoBehaviour
{
    [Header("Positions")]
    public Transform DockingTarget;
    public Transform UndockingTarget;
    public Transform TransformEffects;
    [Header("Effects")]
    public GameObject EffectsPrefab;
    public GameObject BrokeEffectsPrefab;
    [Header("Models")]
    public SkinnedMeshRenderer DetailMeshRenderer;
    public GameObject DetealAdditionalModel;

    public bool DetailUsed = false;


    public void Docking()
    {
        transform.DOMove(DockingTarget.position, 0.1f,false).OnComplete(() => CompleteDocking());
        
    }

    public void CompleteDocking()
    {
        DetailUsed = true;
        Instantiate(EffectsPrefab, TransformEffects);
        DetailMeshRenderer.enabled = true;
        DetealAdditionalModel.SetActive(false);
    }
    public void Breaking()
    {
        Instantiate(BrokeEffectsPrefab, TransformEffects);
        transform.DOJump(UndockingTarget.position, 2, 1, 1, false);
        DetailMeshRenderer.enabled = false;
        DetealAdditionalModel.SetActive(true);
    }
}
