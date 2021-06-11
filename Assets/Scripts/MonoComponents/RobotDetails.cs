using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using TeamAlpha.Source;

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
    private LineRenderer dockingLine;

    private void Start()
    {
        StartUndock();
        rigidbody = GetComponent<Rigidbody>();
        VisualDetail.enabled = false;
    }

    private void Update()
    {
        if (dockingLine) 
        {
            List<Vector3> positions = new List<Vector3>();
            Vector3 startPoint = dockingLine.transform.position;
            Vector3 endPoint = transform.parent.position + dockingLine.transform.localPosition;

            positions.Add(endPoint);
            for (int i = 0; i < DataGameMain.Default.dockingLineBetweenPoints; i++) 
            {
                float strenght = DataGameMain.Default.dockingLineNoiseStrenght;
                float frequency = DataGameMain.Default.dockingLineNoiseFrequency;
                float t = 1f / (DataGameMain.Default.dockingLineBetweenPoints + 2f) * (i + 1f);
                Vector3 betweenPoint = Vector3.Lerp(endPoint, startPoint, t);
                betweenPoint.x += Mathf.PerlinNoise(betweenPoint.x, Time.time * frequency) * strenght;
                betweenPoint.y += Mathf.PerlinNoise(Time.time * frequency, betweenPoint.y) * strenght;
                positions.Add(betweenPoint);
            }
            positions.Add(startPoint);
            dockingLine.positionCount = positions.Count;
            dockingLine.SetPositions(positions.ToArray());
        }
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
        dockingLine = CreateDockingLine()?.GetComponent<LineRenderer>();

        if (forceField) Destroy(forceField);
        VisualDetail.enabled = true;
        transform.DOLocalMove(Vector3.zero, 0.5f)
            .SetEase(Ease.InExpo)
            .OnComplete(() => DetailOnRobot());
    }

    private GameObject CreateDockingLine() 
    {
        GameObject prefab = GetComponentInParent<DetailController>()?.DockingLinePrefab;
        if (prefab)
        {
            GameObject lineObject = Instantiate(prefab);
            lineObject.transform.SetParent(VisualDetail.transform);
            lineObject.transform.localPosition = VisualDetail.GetComponent<BoxCollider>().center;
            lineObject.transform.SetParent(transform);

            return lineObject;
        }
        return null;
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
        if (dockingLine) Destroy(dockingLine.gameObject);
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