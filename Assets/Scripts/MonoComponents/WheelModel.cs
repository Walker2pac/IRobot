using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelModel : MonoBehaviour
{
    public List<WheelsDetails> WheelsdDetails = new List<WheelsDetails>();



    public void ActiveModeleWheel()
    {
        for (int i = 0; i < WheelsdDetails.Count; i++)
        {
            WheelsdDetails[i].DockingDetail();
        }

        /*for (int i = 0; i < ShieldParts.Count; i++)
        {
            ShieldParts[i].enabled = true;
        }*/
    }

    public void UnactiveWheelModel()
    {
        for (int i = 0; i < WheelsdDetails.Count; i++)
        {
            WheelsdDetails[i].UndockingDetail();
        }
        /*for (int i = 0; i < ShieldParts.Count; i++)
        {
            ShieldParts[i].enabled = false;
        }*/
    }
}
