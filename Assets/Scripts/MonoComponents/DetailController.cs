using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DetailType
{
    Hand,
    Torso,
    Foot,
    Base,
}
public enum DetailSide 
{
    Left,
    Right
}

public class DetailController : MonoBehaviour
{
    [Serializable]
    public class Level 
    {
        public DetailType type;
        public List<UpgradeObjectBridge> upgradeObjects = new List<UpgradeObjectBridge>();
    }

    [SerializeField] private GameObject forceFieldPrefab;
    [SerializeField] private GameObject dockingLinePrefab;
    [SerializeField] private GameObject breakedDetailPrefab;
    [Space]
    [SerializeField] private Transform leftPreAttachPoint;
    [SerializeField] private Transform rightPreAttachPoint;
    [Space]
    [SerializeField] private List<Level> levels = new List<Level>();

    private int _curentDetail = 0;
    private int _detailCount;
    private Dictionary<DetailType, List<RobotDetails>> _details;
    private List<RobotDetails> _baseDetails;

    private void Start()
    {
        List<RobotDetails> details = new List<RobotDetails>(GetComponentsInChildren<RobotDetails>());
        _baseDetails = details.FindAll((d) => d.Type == DetailType.Base);

        _details = new Dictionary<DetailType, List<RobotDetails>>();
        _details.Add(DetailType.Hand, details.FindAll((d) => d.Type == DetailType.Hand));
        _details.Add(DetailType.Torso, details.FindAll((d) => d.Type == DetailType.Torso));
        _details.Add(DetailType.Foot, details.FindAll((d) => d.Type == DetailType.Foot));

        _detailCount = details.Count - _baseDetails.Count;
    }

    public bool LoseDetail(int damage)
    {
        if (Shield.Default.Spawned)
            Shield.Default.Break();
        else 
        {
            for (int i = 0; i < damage; i++)
            {
                RobotDetails detail = GetPrevious();
                if (!detail) return false;

                detail.BreakDetail(breakedDetailPrefab);
            }
        }
        return true;
    }

    public void AddDetail()
    {
        RobotDetails detail = GetNext();
        if (detail) 
        {
            Transform point = detail.Side == DetailSide.Left ? leftPreAttachPoint : rightPreAttachPoint;
            detail.AttachDetail(forceFieldPrefab, dockingLinePrefab, point);
        }
    }

    private RobotDetails GetNext() 
    {
        if (_curentDetail > _detailCount - 1)
            return null;

        RobotDetails detail = GetCurrentDetail();
        if (_details[detail.Type].IndexOf(detail) == 0) 
        {
            Level level = levels.Find((l) => l.type == detail.Type);
            foreach (UpgradeObjectBridge b in level.upgradeObjects)
                b.Spawn();
        }
        _curentDetail++;
        return detail;
    }

    private RobotDetails GetPrevious() 
    {
        if (_curentDetail < 0)
            return null;

        RobotDetails detail = GetCurrentDetail();
        _curentDetail--;
        return detail;
    }

    private RobotDetails GetCurrentDetail() 
    {
        int trueIndex = _curentDetail;
        foreach (Level l in levels) 
        {
            if (trueIndex <= _details[l.type].Count - 1)
                return _details[l.type][trueIndex];
            else
                trueIndex -= _details[l.type].Count;
        }
        return null;
    }

    public void DeathEffect() 
    {
        foreach (RobotDetails d in _baseDetails) 
            d.BreakDetail(breakedDetailPrefab, 0f);
        foreach (Level level in levels) 
        {
            foreach (UpgradeObjectBridge upgrade in level.upgradeObjects)
                upgrade.Delete();
        }
    }
}


