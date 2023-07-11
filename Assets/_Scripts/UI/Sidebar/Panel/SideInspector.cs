using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Sidebar
{
    public abstract class SideInspector : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        private Tween _anchorPosTween;

        private void Start()
        {
            HideInstant();
        }

        public virtual void ShowTweened()
        {
            _anchorPosTween.Kill();
            _anchorPosTween = _rectTransform.DOAnchorPosX(0, 0.5f).SetEase(Ease.OutCubic);
        }

        public virtual void HideTweened()
        {
            _anchorPosTween.Kill();
            _anchorPosTween = _rectTransform.DOAnchorPosX(-_rectTransform.rect.size.y - 100, 0.5f).SetEase(Ease.InCubic);
        }

        public virtual void ShowInstant()
        {
            _anchorPosTween.Kill();
            _rectTransform.anchoredPosition = _rectTransform.anchoredPosition.WithX(0);
        }

        public virtual void HideInstant()
        {
            _anchorPosTween.Kill();
            _rectTransform.anchoredPosition = _rectTransform.anchoredPosition.WithX(-_rectTransform.rect.size.y - 100);
        }
    }
}