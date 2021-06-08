using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DetailController : MonoBehaviour
{
    public List<RobotDetails> RobotDetails = new List<RobotDetails>();
    public List<RobotDetails> HandDetails = new List<RobotDetails>();
    public List<RobotDetails> TorsoDetails = new List<RobotDetails>();
    public List<RobotDetails> FootDetails = new List<RobotDetails>();
    public List<RobotDetails> FallenRobotDetails = new List<RobotDetails>();

    public bool AllDetailsFallen;
    public bool HandDetailsDone;
    public bool TorsoDetailsDone;
    public bool FootDetailsDone;

    public int HandDetailNumber;
    public int TorsoDetailNumber;
    public int FootDetailNumber;


    private void Start()
    {
        for (int i = 0; i < FallenRobotDetails.Count; i++)
        {
            if (FallenRobotDetails[i].tag == "HandDetail")
            {
                HandDetailNumber++;
            }
            if (FallenRobotDetails[i].tag == "TorsoDetail")
            {
                TorsoDetailNumber++;
            }
            if (FallenRobotDetails[i].tag == "FootDetail")
            {
                FootDetailNumber++;
            }
        }
    }



    public void FallenDetail(int damage)
    {

        for (int i = 0; i < damage; i++)
        {
            for (int j = RobotDetails.Count - 1; j < RobotDetails.Count; j--)
            {
                RobotDetails[j].UndockingDetail();
                FallenRobotDetails.Add(RobotDetails[j]);
                FallenRobotDetails.Insert(0, RobotDetails[j]);

                if (RobotDetails[j].tag == "HandDetail")
                {
                    HandDetails.RemoveAt(HandDetails.Count - 1);
                    RobotDetails.RemoveAt(j);
                    break;
                }
                if (RobotDetails[j].tag == "TorsoDetail")
                {
                    TorsoDetails.RemoveAt(TorsoDetails.Count - 1);
                    RobotDetails.RemoveAt(j);
                    break;
                }
                if (RobotDetails[j].tag == "FootDetail")
                {
                    FootDetails.RemoveAt(FootDetails.Count - 1);
                    RobotDetails.RemoveAt(j);
                    break;
                }
            }
        }
        CheckDetailsStatus();

    }

    public void AddDetail()
    {

        if (!HandDetailsDone)
        {
            for (int i = 0; i < FallenRobotDetails.Count; i++)
            {
                if (FallenRobotDetails[i].tag == "HandDetail")
                {
                    FallenRobotDetails[i].DockingDetail();
                    HandDetails.Add(FallenRobotDetails[i]);
                    RobotDetails.Add(FallenRobotDetails[i]);
                    FallenRobotDetails.RemoveAt(i);
                    break;
                }
            }
            CheckDetailsOnRobot();
        }
        if (HandDetailsDone)
        {
            for (int i = 0; i < FallenRobotDetails.Count; i++)
            {
                if (FallenRobotDetails[i].tag == "TorsoDetail")
                {
                    FallenRobotDetails[i].DockingDetail();
                    TorsoDetails.Add(FallenRobotDetails[i]);
                    RobotDetails.Add(FallenRobotDetails[i]);
                    FallenRobotDetails.RemoveAt(i);
                    break;
                }
            }
            CheckDetailsOnRobot();
        }
        if (TorsoDetailsDone) ////////
        {
            for (int i = 0; i < FallenRobotDetails.Count; i++)
            {
                if (FallenRobotDetails[i].tag == "FootDetail")
                {
                    FallenRobotDetails[i].DockingDetail();
                    FootDetails.Add(FallenRobotDetails[i]);
                    RobotDetails.Add(FallenRobotDetails[i]);
                    FallenRobotDetails.RemoveAt(i);
                    break;
                }
            }
            CheckDetailsOnRobot();
        }



        else
        {
            Debug.Log("По кайфу");
        }

    }
    void CheckDetailsStatus()
    {
        if (RobotDetails.Count == 0)
        {
            AllDetailsFallen = true;

        }
        else
        {
            AllDetailsFallen = false;
        }
    }

    public void CheckDetailsOnRobot()
    {
        if (HandDetails.Count == HandDetailNumber)
        {
            for (int i = 0; i < HandDetails.Count; i++)
            {
                if (HandDetails[HandDetails.Count - 1].DetailUsed == true)
                {
                    HandDetailsDone = true;
                }
            }
        }

        if (TorsoDetails.Count == TorsoDetailNumber)
        {
            for (int i = 0; i < TorsoDetails.Count; i++)
            {
                if (TorsoDetails[TorsoDetails.Count - 1].DetailUsed == true)
                {
                    TorsoDetailsDone = true;
                }
            }
        }

        if (FootDetails.Count == FootDetailNumber)
        {
            for (int i = 0; i < FootDetails.Count; i++)
            {
                if (FootDetails[FootDetails.Count - 1].DetailUsed == true)
                {
                    FootDetailsDone = true;
                }
            }
        }
    }
}


