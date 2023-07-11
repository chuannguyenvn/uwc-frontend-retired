using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Sidebar
{
    public abstract class SidePanel : MonoBehaviour
    {
        public event Action Shown;
        public event Action Hidden;

        private bool _isShowing;
        public bool IsShowing => _isShowing;

        [SerializeField] private RectTransform _rectTransform;
        private Tween _anchorPosTween;

        private void Start()
        {
            ShowInstant();
            HideInstant();
        }

        public virtual void ShowTweened()
        {
            RegisterAsShown();

            _anchorPosTween.Kill();
            _anchorPosTween = _rectTransform.DOAnchorPosX(0, 0.5f).SetEase(Ease.OutCubic).OnComplete(() => Shown?.Invoke());
        }

        public virtual void HideTweened()
        {
            RegisterAsHidden();


            _anchorPosTween.Kill();
            _anchorPosTween = _rectTransform.DOAnchorPosX(-_rectTransform.rect.size.y - 100, 0.5f)
                .SetEase(Ease.InCubic)
                .OnComplete(() => Hidden?.Invoke());
        }

        public virtual void ShowInstant()
        {
            RegisterAsShown();

            _anchorPosTween.Kill();
            _rectTransform.anchoredPosition = _rectTransform.anchoredPosition.WithX(0);
        }

        public virtual void HideInstant()
        {
            RegisterAsHidden();

            _anchorPosTween.Kill();
            _rectTransform.anchoredPosition = _rectTransform.anchoredPosition.WithX(-_rectTransform.rect.size.y - 100);
        }

        private void RegisterAsShown()
        {
            SidebarManager.Instance.PushBackStack(this);
            _isShowing = true;
        }

        private void RegisterAsHidden()
        {
            SidebarManager.Instance.PopBackStack();
            _isShowing = false;
        }
    }
}