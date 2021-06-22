using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Shield : MonoBehaviour, IUpgradeObject
{
    private static Shield _default;
    public static Shield Default => _default;

    [Serializable] public class ShieldPart 
    {
        public Renderer part;
        public Transform spawnPoint;
        [HideInInspector] public Vector3 defaultPosition;
        [HideInInspector] public Quaternion defaultRotation;
    }
    [SerializeField] private GameObject breakedDetailPrefab;
    [SerializeField] private List<ShieldPart> parts = new List<ShieldPart>();

    public Action OnShieldSpawned;

    private bool _spawned = false;

    public bool Spawned => _spawned;

    private void Awake()
    {
        _default = this;
    }

    private void Start()
    {
        foreach (ShieldPart p in parts) 
        {
            p.part.enabled = false;
            p.defaultPosition = p.part.transform.localPosition;
            p.defaultRotation = p.part.transform.localRotation;
        }
    }

    public void Break()
    {
        _spawned = false;
        foreach (ShieldPart p in parts) 
        {
            p.part.enabled = false;
            GameObject breakedDetail = Instantiate(breakedDetailPrefab);
            breakedDetail.GetComponent<MeshFilter>().sharedMesh = p.part.GetComponent<MeshFilter>().sharedMesh;
            breakedDetail.GetComponent<MeshRenderer>().sharedMaterials = p.part.sharedMaterials;
            breakedDetail.transform.position = p.part.transform.position;
            breakedDetail.transform.rotation = p.part.transform.rotation;
            breakedDetail.AddComponent<MeshCollider>().convex = true;

            Vector3 forceDirection = (breakedDetail.transform.position - transform.position).normalized;
            Rigidbody rb = breakedDetail.GetComponent<Rigidbody>();
            rb.AddForce(3 * forceDirection, ForceMode.Impulse);
            rb.AddTorque(Vector3.up * 3f, ForceMode.Impulse);

            Destroy(breakedDetail, 5f);
        }
    }

    #region IUpgradeObject
    public void Spawn() 
    {
        if (!_spawned)
        {
            _spawned = true;
            OnShieldSpawned?.Invoke();
            int rotationSign = 1;
            foreach (ShieldPart p in parts)
            {
                Transform part = p.part.transform;

                p.part.enabled = true;
                part.localScale = Vector3.one * 0.01f;
                part.localRotation = Quaternion.Euler(0, 90 * rotationSign, 0);
                part.position = p.spawnPoint.position;
                part.transform.SetParent(p.spawnPoint);

                Tween scaleTween = part.DOScale(Vector3.one, 0.5f)
                    .SetEase(Ease.OutBack);

                Tween moveTween = DOTween.To(
                    () => 0f,
                    (v) =>
                    {
                        part.position = Vector3.Lerp(part.position, transform.TransformPoint(p.defaultPosition), v);
                        part.rotation = Quaternion.Lerp(part.rotation, transform.rotation * p.defaultRotation, v);
                    },
                    1f, 0.5f)
                    .SetEase(Ease.InExpo);

                DOTween.Sequence()
                    .Append(scaleTween)
                    .Append(moveTween)
                    .OnComplete(() => part.transform.SetParent(transform));

                rotationSign *= -1;
            }
        }
    }

    public void Delete() {}
    #endregion
}
