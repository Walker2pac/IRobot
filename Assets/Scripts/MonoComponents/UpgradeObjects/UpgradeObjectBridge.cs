using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IUpgradeObject))]
public class UpgradeObjectBridge : MonoBehaviour
{
    private IUpgradeObject _target;

    private void Start() =>
        _target = GetComponent<IUpgradeObject>();

    public void Spawn() =>
        _target.Spawn();

    public void Delete() =>
        _target.Delete();
}
