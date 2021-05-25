using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRobotStates : MonoBehaviour
{
    private Animator _animator;
    public List<DetailScript> Details = new List<DetailScript>();
    public int CurrentStateIndex = 0;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            for (int i = 0; i < Details.Count; i++)
            {
                if (i == CurrentStateIndex)
                {
                    Details[i].Docking();
                    Invoke("SetAnimation", 1f);
                }
            }
            CurrentStateIndex++;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            CurrentStateIndex--;
            for (int i = 0; i < Details.Count; i++)
            {
                if (i == CurrentStateIndex)
                {
                    Details[i].Breaking();
                    SetAnimation();

                }
            }            
        }
    }

    void SetAnimation()
    {
        _animator.SetInteger("RobotState", CurrentStateIndex -1);
    }


}
