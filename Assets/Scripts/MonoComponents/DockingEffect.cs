using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingEffect : MonoBehaviour
{
    public GameObject Effect;
    public GameObject BrokeEffect;

    public void ShowEfffect()
    {
        Instantiate(Effect, transform);
    }
    public void ShowBrokeEfffect()
    {
        Instantiate(BrokeEffect, transform);
    }
}
