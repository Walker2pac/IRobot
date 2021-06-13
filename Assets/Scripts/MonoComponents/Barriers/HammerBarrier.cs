using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace TeamAlpha.Source
{
    public class HammerBarrier : MonoBehaviour
    {
        [SerializeField] private Transform movingAxis;
        [Space, SerializeField] private float moveDownSpeed;
        [SerializeField] private float moveUpSpeed;
        [SerializeField] private float delay;
        [SerializeField] private float timeOffset;
        [Space, SerializeField] private int collisionDamage;
        [SerializeField] private int hitDamage;

        private int damage;

        private void Start()
        {
            StartCoroutine(DelayedPunch(timeOffset));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == DataGameMain.LayerPlayer)
            {
                PlayerController.Current.SendDamage(damage);
                Destroy(gameObject);
            }
        }

        private IEnumerator DelayedPunch(float time)
        {
            yield return new WaitForSeconds(time);
            MoveDown();
        }

        private void MoveDown()
        {
            damage = hitDamage;
            movingAxis.DOLocalRotate(Vector3.up * -90, moveDownSpeed)
                .SetEase(Ease.InQuint)
                .OnComplete(MoveUp);
        }

        private void MoveUp()
        {
            damage = collisionDamage;
            movingAxis.DOLocalRotate(Vector3.up * 0, moveUpSpeed)
                .SetEase(Ease.OutBack)
                .OnComplete(() => StartCoroutine(DelayedPunch(delay)));
        }
    }
}