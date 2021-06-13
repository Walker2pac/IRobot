using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using TeamAlpha.Source;

public class RobotDetails : MonoBehaviour
{
    [SerializeField] private DetailType _type;

    private Vector3 _defaultParentPosition;
    private Transform _defaultParent;
    private Renderer _renderer;

    public DetailType Type => _type;

    private void Start()
    {
        _defaultParentPosition = transform.localPosition;
        _defaultParent = transform.parent;
        _renderer = GetComponent<Renderer>();
    }

    public void AttachDetail() 
    {

    }

    public void BreakDetail() 
    {

    }

/*    public void ShowDetail()
    {
        dockingLine = CreateDockingLine()?.GetComponent<LineRenderer>();
        if (forceField) Destroy(forceField);
        VisualDetail.enabled = true;

        Vector3 startPosition = transform.localPosition;
        DOTween.To(
            () => 0f,
            (v) => 
                {
                    transform.localPosition = Vector3.Lerp(startPosition, Vector3.zero, v);
                    CalculateDockingLine(v);
                },
            1f, 1f)
            .SetEase(Ease.InExpo)
            .OnComplete(() => DetailOnRobot());   
    }

    private void CalculateDockingLine(float tweenValue) 
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
                betweenPoint.x += (0.5f - Mathf.PerlinNoise(betweenPoint.x, Time.time * frequency)) * strenght * (1 - tweenValue);
                betweenPoint.y += (0.5f - Mathf.PerlinNoise(Time.time * frequency, betweenPoint.y)) * strenght * (1 - tweenValue);
                positions.Add(betweenPoint);
            }
            positions.Add(startPoint);
            dockingLine.positionCount = positions.Count;
            dockingLine.SetPositions(positions.ToArray());
        }
    }

    private GameObject CreateDockingLine()
    {
        GameObject prefab = detailController?.DockingLinePrefab;
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
        GameObject prefab = detailController?.ForceFieldPrefab;
        if (prefab)
        {
            GameObject forceObject = Instantiate(prefab);
            forceObject.transform.SetParent(VisualDetail.transform);
            forceObject.transform.localPosition = VisualDetail.GetComponent<BoxCollider>().center;
            forceObject.transform.SetParent(transform);

            return forceObject;
        }
        return null;
    }*/
}