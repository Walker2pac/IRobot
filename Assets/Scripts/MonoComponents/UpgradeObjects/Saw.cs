using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour, IUpgradeObject
{
    [SerializeField] private GameObject metarig;
    [SerializeField] private GameObject shield;
    [SerializeField] private Transform saw;
    [SerializeField] private ParticleSystem trail;
    [SerializeField] private float sawSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float duration;

    private bool _spawned;
    private static Saw _default;
    private List<Renderer> _renderers;

    public bool Spawned => _spawned;
    public static Saw Default => _default;
    public float MoveSpeed => moveSpeed;

    public Action OnSpawned = () => { };
    public Action OnDeleted = () => { };

    void Awake() 
    {
        _default = this;
    }

    void Start()
    {
        _renderers = new List<Renderer>(GetComponentsInChildren<Renderer>());
        foreach (Renderer r in _renderers)
            r.enabled = false;
    }

    void Update()
    {
        if (_spawned)
            saw.localRotation *= Quaternion.Euler(Vector2.right * sawSpeed);
    }

    private void SwitchState(bool arg) 
    {
        if (_spawned == arg) return;

        _spawned = arg;
        metarig.SetActive(!arg);
        shield.SetActive(!arg);
        foreach (Renderer r in _renderers)
            r.enabled = arg;

        if (arg)
        {
            trail.Play();
            OnSpawned?.Invoke();
        }
        else 
        {
            trail.Stop();
            OnDeleted?.Invoke();
        }
    }

    private IEnumerator DeleteCourutine() 
    {
        yield return new WaitForSeconds(duration);
        Delete();
    }

    #region IUpgradeObject

    public void Delete()
    {
        SwitchState(false);
    }

    public void Spawn()
    {
        SwitchState(true);
        StartCoroutine(DeleteCourutine());
    }

    #endregion
}
