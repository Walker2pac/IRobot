using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//using UnityEngine.Events;
using TeamAlpha.Source;

public class RobotDetails : MonoBehaviour
{
    [SerializeField] private DetailType _type;
    [SerializeField] private DetailSide _side;

    private Vector3 _defaultParentPosition;
    private Vector3 _defaultParentScale;
    private Quaternion _defaultParentRotation;
    private Transform _defaultParent;
    private Transform _preAttachPoint;
    private Renderer _renderer;
    private MeshFilter _mesh;
    private LineRenderer _dockingLine;
    private Sequence _attachSequence;
    public DetailType Type => _type;
    public DetailSide Side => _side;

    bool damage;

    private void Start()
    {
        _defaultParentPosition = transform.localPosition;
        _defaultParentScale = transform.localScale;
        _defaultParentRotation = transform.localRotation;
        _defaultParent = transform.parent;
        _renderer = GetComponent<Renderer>();
        _mesh = GetComponent<MeshFilter>();

        _renderer.enabled = _type == DetailType.Base;
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
    public void DettachDetail(GameObject dockingLinePrefab, Transform preAttachPoint)
    {
        _preAttachPoint = preAttachPoint;
        transform.SetParent(preAttachPoint);
        transform.localScale = Vector3.one;
        Instantiate(dockingLinePrefab,transform);
        /*_dockingLine.transform.localPosition = Vector3.zero;
        _dockingLine.positionCount = 0;*/
        Tween moveTween = DOTween.To(
            () => 0f,
            (v) =>
            {
                transform.position = Vector3.Lerp(transform.position, _preAttachPoint.position, 0.1f);
                //transform.DOScale(Vector3.zero, 0.5f);
                transform.rotation = Quaternion.Lerp(transform.rotation, _defaultParent.rotation * _defaultParentRotation, 0.1f);
                //CalculateLine(0.9f);
            },
            1f, 0.5f)
            .SetEase(Ease.InExpo).OnComplete(() => EndDettach());

    }

    void EndDettach()
    {
        transform.DOScale(Vector3.zero, 0.5f);
        if (_dockingLine)
        {
            Destroy(_dockingLine.gameObject);
            _dockingLine = null;
        }
        
    }
    public void BreakDetail(GameObject breakedDetailPrefab, float force = 0.01f)
    {
        damage = true;

        if (_attachSequence != null) EndAttach();
        _renderer.enabled = false;
        GameObject breakedDetail = Instantiate(breakedDetailPrefab);
        breakedDetail.GetComponent<MeshFilter>().sharedMesh = _mesh.sharedMesh;
        breakedDetail.GetComponent<MeshRenderer>().material = _renderer.material;
        breakedDetail.transform.position = transform.position;
        breakedDetail.transform.rotation = transform.rotation;
        Vector3 forceDirection = (breakedDetail.transform.position - PlayerController.Current.transform.position).normalized;
        EndAttach();
        StartCoroutine(DestroyBrokedDetail(breakedDetail));
    }

    private IEnumerator DestroyBrokedDetail(GameObject d)
    {
        yield return new WaitForSeconds(0.8f);
        damage = false;
        yield return new WaitForSeconds(2f);

        d.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 1f)
            .SetEase(Ease.InBack).SetUpdate(UpdateType.Normal, true);
        StartCoroutine(DestoyDetails(d, 4f));
    }

    IEnumerator DestoyDetails(GameObject d, float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(d);
    }

    public void StartAttach()
    {
        _renderer.enabled = true;

        Vector3 scale = damage ? Vector3.one * 0.01f : Vector3.one;
        Tween scaleTween = transform.DOScale(scale, 0.5f)
            .SetEase(Ease.OutBack);

        Tween moveTween = DOTween.To(
            () => 0f,
            (v) =>
            {

                transform.position = Vector3.Lerp(transform.position, _defaultParent.TransformPoint(_defaultParentPosition), v);
                transform.rotation = Quaternion.Lerp(transform.rotation, _defaultParent.rotation * _defaultParentRotation, v);
                CalculateLine(0.9f);
            },
            1f, 0.5f)
            .SetEase(Ease.InExpo);

        _attachSequence = DOTween.Sequence()
            .Append(scaleTween)
            .Append(moveTween)
            .OnComplete(EndAttach);
    }

    
    private void EndAttach()
    {
        _attachSequence.Kill();
        _attachSequence = null;
        transform.SetParent(_defaultParent);
        transform.localPosition = _defaultParentPosition;
        transform.localScale = _defaultParentScale;
        transform.localRotation = _defaultParentRotation;

        if (_dockingLine)
        {
            Destroy(_dockingLine.gameObject);
            _dockingLine = null;
        }
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

}