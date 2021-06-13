using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IUpgradeObject
{
    void Start()
    {
        
    }


    #region IUpgradeObject
    public void Spawn() { }

    public void Delete() { }
    #endregion
}
