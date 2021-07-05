using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamAlpha.Source;
using DG.Tweening;

public class GunUpgradeObject : MonoBehaviour, IUpgradeObject
{
    [SerializeField] private Transform gunModel;
    [SerializeField] private Transform gunSegmentModel;

    private bool _spawned;
    private bool _finished;
    private List<Renderer> _partRenderers;
    private Gun _gun;


    void Start()
    {        
        _partRenderers = new List<Renderer>();
        _partRenderers.AddRange(GetComponentsInChildren<Renderer>());
        foreach (Renderer r in _partRenderers)
            r.enabled = false;

        _gun = GetComponent<Gun>();
        _gun.isShooting = false;
        _gun.onShoot += ShootAnim;
    }

    void LateUpdate()
    {
        if (_spawned) 
        {
            gunModel.rotation = PlayerController.Current.transform.rotation;
        }
        if (_finished)
        {
            gunModel.localRotation = Quaternion.Euler(Vector3.zero);
            
        }
    }

    private void ShootAnim() 
    {
        {
            gunSegmentModel.DOLocalRotate(Vector3.right * -50f, 0.2f)
            .SetEase(Ease.OutQuint)
            .OnComplete(() =>
            {
                gunSegmentModel.DOLocalRotate(Vector3.right * 30f, 1f);
            });
        }
        
    }

    public void Finish()
    {
        _finished = true;
        _spawned = false;
        _gun.StopAllCoroutines();
        Delete();
    }

    #region IUpgradeObject
    public void Delete()
    {
        if (!_spawned) return;

        _spawned = false;
        _gun.isShooting = false;
        foreach (Renderer r in _partRenderers)
            r.enabled = false;
    }

    public void Spawn()
    {
        if (_spawned) return;

        _spawned = true;
        _gun.isShooting = true;
        foreach (Renderer r in _partRenderers)
            r.enabled = true;
    }
    #endregion
}
