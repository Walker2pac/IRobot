using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingEffect : MonoBehaviour
{
    public GameObject Effect;

    public void ShowEfffect()
    {
        Instantiate(Effect, transform);
    }
}
