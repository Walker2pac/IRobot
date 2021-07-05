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
    Right,
    Centr
}


namespace TeamAlpha.Source
{
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
        [SerializeField] private GameObject outlineDetailPrefab;
        [Space]
        [SerializeField] private Transform leftPreAttachPoint;
        [SerializeField] private Transform rightPreAttachPoint;
        [SerializeField] private Transform dettachPoint;
        [Space]
        [SerializeField] private List<Level> levels = new List<Level>();
        private int _curentDetail = 0;
        private int _detailCount;
        private Dictionary<DetailType, List<RobotDetails>> _details;
        private List<RobotDetails> _baseDetails;
        int allHavenDetail;

        private void Start()
        {
            List<RobotDetails> details = new List<RobotDetails>(GetComponentsInChildren<RobotDetails>());
            Debug.Log(details);
            _baseDetails = details.FindAll((d) => d.Type == DetailType.Base);

            _details = new Dictionary<DetailType, List<RobotDetails>>();
            _details.Add(DetailType.Hand, details.FindAll((d) => d.Type == DetailType.Hand));
            _details.Add(DetailType.Torso, details.FindAll((d) => d.Type == DetailType.Torso));
            _details.Add(DetailType.Foot, details.FindAll((d) => d.Type == DetailType.Foot));

            _detailCount = details.Count - _baseDetails.Count;
        }

        public bool LoseDetail(int damage)
        {
            if (Saw.Default.Spawned)
                return true;

            if (Shield.Default.Spawned)
                Shield.Default.Break();

            else
            {
                for (int i = 0; i < damage; i++)
                {
                    RobotDetails detail = GetPrevious();
                    if (!detail) return false;

                    detail.BreakDetail(breakedDetailPrefab);
                    allHavenDetail--;
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
                allHavenDetail++;
            }
        }

        public void DettachDetail()
        {
            StartCoroutine(DettachOneDetail());
        }

        IEnumerator DettachOneDetail()
        {
            float detailRatio = (float)_detailCount / FindObjectOfType<GirlController>().girlDetails.Count;
            float a = (float)allHavenDetail / detailRatio;
            int numberAttachDetail = Mathf.RoundToInt(a);
            int b = 0;
            for (int i = 0; i < allHavenDetail; i++)
            {
                RobotDetails detail = GetPrevious();
                if (detail)
                {
                    detail.DettachDetail(dockingLinePrefab, dettachPoint);
                    b++;
                    yield return new WaitForSeconds(0.2f);
                    if (b >= detailRatio)
                    {
                        FindObjectOfType<GirlController>().AttachDetail();
                        b = 0;
                    }
                }

            }
        }


        private RobotDetails GetNext()
        {
            if (_curentDetail >= _detailCount - 1)
            {
                return null;
            }
            else
            {
                _curentDetail++;
                RobotDetails detail = GetCurrentDetail();

                if (_details[detail.Type].IndexOf(detail) == _details[detail.Type].Count - 1)
                {
                    Level level = levels.Find((l) => l.type == detail.Type);
                    foreach (UpgradeObjectBridge b in level.upgradeObjects)
                        b.Spawn();
                }
                return detail;
            }
        }

        private RobotDetails GetPrevious()
        {
            if (_curentDetail < 0)
            {
                return null;
            }
            else
            {
                RobotDetails detail = GetCurrentDetail();
                _curentDetail--;
                return detail;
            }
        }

        private RobotDetails GetCurrentDetail()
        {
            int trueIndex = _curentDetail;

            foreach (Level l in levels)
            {
                if (trueIndex <= _details[l.type].Count - 1 && trueIndex >= 0)
                    return _details[l.type][trueIndex];
                else
                {
                    if (trueIndex >= 0)
                    {
                        trueIndex -= _details[l.type].Count;
                    }
                    else
                    {
                        Debug.Log("BigDetail");
                    }
                }
            }
            return null;

        }

        public void DeathEffect()
        {
            for (int i = 0; i < _baseDetails.Count; i++)
            {
                _baseDetails[i].BreakDetail(breakedDetailPrefab, 0f);
            }
            foreach (Level level in levels)
                foreach (UpgradeObjectBridge upgrade in level.upgradeObjects)
                    upgrade.Delete();
        }
    }


}