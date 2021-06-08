using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public enum DetailStates
{
    None,
    StartDocking,
    RedyToDocking,
    DockingComplete,
    Undocking
}

public class RobotDetails : MonoBehaviour
{
    [Header("Renders")]
    public SkinnedMeshRenderer DetailsOnRobot;
    public SkinnedMeshRenderer VisualDetail;
    [Header("Positions")]
    public Transform UndockingTarget;
    public Transform DockingTarget;
    public Transform DetailPosition;

    public Transform PreDockingPosition;
    [Header("States")]
    public DetailStates DetailStates;
    [Header("Effects")]
    public bool UseEffect;
    public UnityEvent EffectDocking;
    public UnityEvent EffectUndocking;

    public bool DetailUsed;


    public bool Left;
    Rigidbody rigidbody;

    private void Start()
    {
        StartUndock();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (DetailStates == DetailStates.StartDocking)
        {
            transform.position = Vector3.MoveTowards(transform.position, PreDockingPosition.position, Time.deltaTime * 25f);
            if (transform.position == PreDockingPosition.position)
            {
                DetailStates = DetailStates.RedyToDocking;
            }

        }

        if (DetailStates == DetailStates.RedyToDocking)
        {
            transform.position = Vector3.MoveTowards(transform.position, DockingTarget.position, Time.deltaTime * 3f);
            if (transform.position == DockingTarget.position)
            {
                DetailOnRobot();
            }
        }

    }

    public void UndockingDetail()
    {
        DetailUsed = false;
        DetailsOnRobot.enabled = false;
        VisualDetail.enabled = true;
        //transform.DOJump(UndockingTarget.position, 2, 1, 1, false).OnComplete(() => SetPosition());
        /*float x = Random.Range(-1, 2);
        float z = Random.Range(-1, 2);
        */
        //boxCollider.enabled = true;
        //rigidbody.useGravity = true;
        // rigidbody.AddForce(Vector3.one, ForceMode.Impulse);
        //rigidbody.velocity = Vector3.one;
        //rigidbody.angularVelocity = Vector3.one;
        //Invoke("SetPosition", 1f);
        
        float x = Random.Range(-1, 2);
        float y = Random.Range(1, 3);
        float z = Random.Range(-1, 2);
        Vector3 random = new Vector3(x, y, z);
        EffectUndocking.Invoke();
        rigidbody.AddForce(random * 40, ForceMode.Impulse);
        rigidbody.useGravity = true;
        //GetComponentInChildren<Collider>().enabled = true;
        Invoke("SetPosition", 2f);
    }

    void StartUndock()
    {
        DetailUsed = false;
        DetailsOnRobot.enabled = false;
        VisualDetail.enabled = true;
        transform.DOJump(UndockingTarget.position, 2, 1, 1, false).OnComplete(() => SetPosition());
    }

    public void DockingDetail()
    {
        DetailStates = DetailStates.StartDocking;
    }

    void DetailOnRobot()
    {
        DetailUsed = true;
        DetailStates = DetailStates.DockingComplete;
        if (UseEffect)
        {
            EffectDocking.Invoke();
        }
        DetailsOnRobot.enabled = true;
        VisualDetail.enabled = false;

    }

    void SetPosition()
    {
        rigidbody.useGravity = false;
        GetComponentInChildren<Collider>().enabled = false;
        transform.position = DetailPosition.position;
    }

}