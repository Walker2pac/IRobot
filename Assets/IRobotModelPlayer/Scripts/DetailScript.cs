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
    public MeshRenderer GunModel;


    public SkinnedMeshRenderer DetealAdditionalModel;
    public MeshRenderer GunAdditionalModel;

    public bool DetailUsed = false;


    public void Docking()
    {
        transform.DOMove(DockingTarget.position, 0.25f,false).OnComplete(() => CompleteDocking());        
    }

    public void CompleteDocking()
    {
        DetailUsed = true;
        Instantiate(EffectsPrefab, TransformEffects);
        DetailMeshRenderer.enabled = true;
        GunModel.enabled = true;

        DetealAdditionalModel.enabled = false;
        GunAdditionalModel.enabled = false;
        
        

    }
    public void Breaking()
    {
        Instantiate(BrokeEffectsPrefab, TransformEffects);
        transform.DOJump(UndockingTarget.position, 2, 1, 1, false);
        DetailMeshRenderer.enabled = false;
        GunModel.enabled = false;

        DetealAdditionalModel.enabled = true;
        GunAdditionalModel.enabled = true;
    }
}
