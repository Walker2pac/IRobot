using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShieldModel : MonoBehaviour
{
    public List<RobotDetails> ShieldDetails = new List<RobotDetails>();
    public UnityEvent EffectUpgrade;

    public void ActiveModeleShield()
    {
        EffectUpgrade.Invoke();
        for (int i = 0; i < ShieldDetails.Count; i++)
        {
            ShieldDetails[i].DockingDetail();
        }
        //Invoke("Active", 0.3f);
    }

    void Active()
    {
        for (int i = 0; i < ShieldDetails.Count; i++)
        {
            ShieldDetails[i].DockingDetail();
        }
    }

    public void UnactiveShieldModel()
    {
        for (int i = 0; i < ShieldDetails.Count; i++)
        {
            ShieldDetails[i].UndockingDetail();
        }
    }
}
