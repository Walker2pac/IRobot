using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TeamAlpha.Source 
{
    public class GirlOutlineDetail : MonoBehaviour
    {

        private Outline outline;

        private void Start()
        {
            outline = GetComponent<Outline>();
            outline.OutlineWidth = 0f;
            outline.OutlineColor = new Color(1, 1, 1, 1);
            StartCoroutine(ShowOutline());
        }

        IEnumerator ShowOutline()
        {
            Tween tweenWidth = DOTween.To(x => outline.OutlineWidth = x, 0, 7.5f, 0.5f);
            yield return new WaitForSeconds(0.1f);
            Tween tweenColor = DOTween.ToAlpha(() => outline.OutlineColor, c => outline.OutlineColor = c, 0, 0.25f);
        }
    }
}