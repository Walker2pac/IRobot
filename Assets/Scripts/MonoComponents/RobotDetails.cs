using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using TeamAlpha.Source;

public class RobotDetails : MonoBehaviour
{
    [SerializeField] private DetailType _type;
    [SerializeField] private DetailSide _side;

    private Vector3 _defaultParentPosition;
    private Quaternion _defaultParentRotation;
    private Transform _defaultParent;
    private Transform _preAttachPoint;
    private Renderer _renderer;
    private LineRenderer _dockingLine;

    public DetailType Type => _type;
    public DetailSide Side => _side;

    private void Start()
    {
        _defaultParentPosition = transform.localPosition;
        _defaultParentRotation = transform.localRotation;
        _defaultParent = transform.parent;
        _renderer = GetComponent<Renderer>();

        _renderer.enabled = false;
    }

    public void AttachDetail(GameObject forceFieldPrefab, GameObject dockingLinePrefab, Transform preAttachPoint) 
    {
        _preAttachPoint = preAttachPoint;

        transform.SetParent(preAttachPoint);
        transform.localScale = Vector3.one * 0.01f;
        transform.position = preAttachPoint.position;

        GameObject forceField = Instantiate(forceFieldPrefab);
        forceField.transform.SetParent(transform);
        forceField.transform.localPosition = Vector3.zero;

        _dockingLine = Instantiate(dockingLinePrefab).GetComponent<LineRenderer>();
        _dockingLine.transform.SetParent(transform);
        _dockingLine.transform.localPosition = Vector3.zero;
        _dockingLine.positionCount = 0;
    }

    public void StartAttach() 
    {
        _renderer.enabled = true;

        Tween scaleTween = transform.DOScale(Vector3.one, 0.5f)
            .SetEase(Ease.OutBack);

        Tween moveTween = DOTween.To(
            () => 0f,
            (v) => 
            {
                transform.position = Vector3.Lerp(_preAttachPoint.position, _defaultParent.TransformPoint(_defaultParentPosition), v);
                CalculateLine(v);
            },
            1f, 1f)
            .SetEase(Ease.InExpo);

        DOTween.Sequence()
            .Append(scaleTween)
            .Append(moveTween)
            .OnComplete(() => 
            {
                transform.SetParent(_defaultParent);
                transform.localRotation = _defaultParentRotation;
                Destroy(_dockingLine.gameObject);
                _dockingLine = null;
            });
    }

    private void CalculateLine(float tweenValue)
    {
        if (_dockingLine)
        {
            List<Vector3> positions = new List<Vector3>();
            Vector3 startPoint = _dockingLine.transform.position;
            Vector3 endPoint = _defaultParent.TransformPoint(_defaultParentPosition);

            positions.Add(endPoint);
            for (int i = 0; i < DataGameMain.Default.dockingLineBetweenPoints; i++)
            {
                float strenght = DataGameMain.Default.dockingLineNoiseStrenght;
                float frequency = DataGameMain.Default.dockingLineNoiseFrequency;
                float t = 1f / (DataGameMain.Default.dockingLineBetweenPoints + 2f) * (i + 1f);
                Vector3 betweenPoint = Vector3.Lerp(endPoint, startPoint, t);
                /*betweenPoint.x += (0.5f - Mathf.PerlinNoise(betweenPoint.x, Time.time * frequency)) * strenght * (1 - tweenValue);*/
                Vector3 normal = (endPoint - startPoint).normalized;
                normal.y *= -1;
                betweenPoint.y += (0.5f - Mathf.PerlinNoise(Time.time * frequency, betweenPoint.y)) * strenght * (1 - tweenValue) * normal.y;
                positions.Add(betweenPoint);
            }
            positions.Add(startPoint);
            _dockingLine.positionCount = positions.Count;
            _dockingLine.SetPositions(positions.ToArray());
        }
    }

    public void BreakDetail() 
    {
        _renderer.enabled = false;
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