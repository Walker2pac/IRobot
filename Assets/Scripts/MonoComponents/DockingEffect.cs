using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamAlpha.Source;

public class DockingEffect : MonoBehaviour
{
    public void ShowEfffect()
    {
        GameObject effect = Instantiate(DataGameMain.Default.attachEffect);
        effect.transform.SetParent(transform);
        effect.transform.localPosition = Vector3.zero;
    }
    public void ShowBrokeEfffect()
    {
        GameObject effect = Instantiate(DataGameMain.Default.brokeEffect);
        effect.transform.SetParent(transform);
        effect.transform.localPosition = Vector3.zero;
    }
}
