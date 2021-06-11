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
    public Transform DockingTarget;
    public Transform DetailPosition;
    public Transform PreDockingPosition;

    [Header("States")]
    public DetailStates DetailStates;

    [Header("Effects")]
    public bool UseEffect;
    public UnityEvent EffectDocking;
    public UnityEvent EffectUndocking;
    
    public bool DetailUsed;
    public bool Left;

    private Rigidbody rigidbody;
    private GameObject forceField;

    private void Start()
    {
        StartUndock();
        rigidbody = GetComponent<Rigidbody>();
        VisualDetail.enabled = false;
    }

    private void Update()
    {
/*        if (DetailStates == DetailStates.StartDocking)
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
*/
    }

    public void UndockingDetail()
    {
        DetailUsed = false;
        DetailsOnRobot.enabled = false;
        VisualDetail.enabled = true;        
        float side = Random.Range(-1, 2);
        float up = Random.Range(1, 3);
        Vector3 random = new Vector3(side, up, side);
        EffectUndocking.Invoke();
        rigidbody.isKinematic = false;
        rigidbody.AddForce(random * 40, ForceMode.Impulse);
        rigidbody.useGravity = true;
        Invoke("SetPosition", 2f);
    }

    void StartUndock()
    {
        DetailUsed = false;
        DetailsOnRobot.enabled = false;
        VisualDetail.enabled = true;
        transform.position = DetailPosition.position;
    }

    public void ShowDetail()
    {
        if (forceField) Destroy(forceField);
        VisualDetail.enabled = true;
        transform.DOLocalMove(Vector3.zero, 0.5f)
            .SetEase(Ease.InExpo)
            .OnComplete(() => DetailOnRobot());
    }

    private GameObject CreateForceField()
    {
        GameObject prefab = GetComponentInParent<DetailController>()?.ForceFieldPrefab;
        if (prefab)
        {
            GameObject forceObject = Instantiate(prefab);
            forceObject.transform.SetParent(VisualDetail.transform);
            forceObject.transform.localPosition = VisualDetail.GetComponent<BoxCollider>().center;
            forceObject.transform.SetParent(transform);

            return forceObject;
        }
        return null;
    }

    public void DockingDetail()
    {
        DetailStates = DetailStates.StartDocking;
        transform.localPosition = PreDockingPosition.localPosition;
        VisualDetail.enabled = false;

        forceField = CreateForceField();
        if (!forceField) ShowDetail();
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
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;
        transform.position = DetailPosition.position;
    }

}