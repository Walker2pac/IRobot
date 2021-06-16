using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TeamAlpha.Source
{
    public class GunBarrier : MonoBehaviour
    {
        [Header ("Objects")]
        [SerializeField] private GameObject gunTop;
        [SerializeField] private Bullet bullet;
        [SerializeField] private GameObject gunMuzzle;
        [SerializeField] private List<GameObject> bulletSpawns = new List<GameObject>();

        [Header("Settings")]
        [SerializeField] private float shotPeriod;
        [Range(0.002f, 0.008f)]
        [SerializeField] private float recoilForce;
        private Transform _player { get => PlayerController.Current.transform; }
        private Vector3 _recoilPosition;
        private Vector3 _startMuzzlePosition;
        private void Start()
        {
            _startMuzzlePosition = gunMuzzle.transform.localPosition;
            _recoilPosition = new Vector3(-recoilForce, gunMuzzle.transform.localPosition.y, -recoilForce);
            StartCoroutine(Shot());
        }
        private void FixedUpdate()
        {
            Vector3 relativePos = _player.position - gunTop.transform.position;
            Vector3 toPlayer = new Vector3(relativePos.x, 0, relativePos.z);
            Quaternion rotation = Quaternion.LookRotation(toPlayer, Vector3.up);
            gunTop.transform.localRotation = rotation;
        }

        private IEnumerator Shot()
        {
            yield return new WaitForSeconds(shotPeriod);
            Debug.Log("Shot");
            gunMuzzle.transform.DOLocalMove(_recoilPosition, 0.1f, false).OnComplete(() => gunMuzzle.transform.DOLocalMove(_startMuzzlePosition, 0.7f, false));
            for (int i = 0; i < bulletSpawns.Count; i++)
            {
                GameObject newBullet = Instantiate(bullet.gameObject, bulletSpawns[i].transform);
                newBullet.transform.SetParent(LevelController.Current.transform);
                float bulletSpeed = DataGameMain.Default.bulletSpeed;
                newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 20, ForceMode.VelocityChange);
            }
            StartCoroutine(Shot());
        }
    }
}


