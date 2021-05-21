using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamagableBody : MonoBehaviour
{
    public abstract void DamagedReaction();
    public abstract void NonDamagedReaction();
}
