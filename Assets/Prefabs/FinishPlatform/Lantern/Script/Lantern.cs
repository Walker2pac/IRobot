using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public enum RotateDirection
{
    Left,
    Right
}

public class Lantern : MonoBehaviour
{
    [Header ("Move")]
    [SerializeField] private bool randomValues;
    [ShowIf("@!randomValues"), SerializeField] private RotateDirection currentRoteteDirection;
    [ShowIf("@!randomValues"), SerializeField, Range(10,30)] private float rotateSpeed;
    [ShowIf("@!randomValues"), SerializeField, Range(40,60)] private float spotlightAngle;
    [ShowIf("@!randomValues"), SerializeField, Range(1,4)] private float speedSpotlightTilt;
    [Header("Spotlight Model")]
    [SerializeField] private GameObject spotlight;

    [Header("Light")]
    [SerializeField] private Color currentColor;
    [SerializeField] private List<Renderer> spotlightRenders = new List<Renderer>();
     
    


    private void Start()
    {
        OneSide();
        for (int i = 0; i < spotlightRenders.Count; i++)
        {   
            spotlightRenders[i].material.color = currentColor;
        }

        if (randomValues)
        {
            rotateSpeed = Random.Range(10, 30);
            spotlightAngle = Random.Range(40, 60);
            speedSpotlightTilt = Random.Range(1, 4);
            int randomDirection = Random.Range(1, 3);
            if (randomDirection == 1)
            {
                currentRoteteDirection = RotateDirection.Left;
            }
            else
            {
                currentRoteteDirection = RotateDirection.Right;
            }
        }
    }
    private void Update()
    {

        if(currentRoteteDirection == RotateDirection.Left)
        {
            transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
        }
        else
        {
            transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0));
        }
         
    }

    void OneSide()
    {
        spotlight.transform.DOBlendableLocalRotateBy(new Vector3(-spotlightAngle * 2, 0, 0), speedSpotlightTilt, RotateMode.WorldAxisAdd).OnComplete(() => TwoSide());
    }

    void TwoSide()
    {
        spotlight.transform.DOBlendableLocalRotateBy(new Vector3(spotlightAngle * 2, 0, 0), speedSpotlightTilt, RotateMode.WorldAxisAdd).OnComplete(() => OneSide());
    }

}
