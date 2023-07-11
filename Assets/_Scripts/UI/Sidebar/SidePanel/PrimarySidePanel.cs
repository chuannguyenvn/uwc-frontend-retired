using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Sidebar
{
    public abstract class PrimarySidePanel : MonoBehaviour
    {
        public SidePanelType SidePanelType;
        
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
            _anchorPosTween = _rectTransform.DOAnchorPosX(0, VisualManager.Instance.SIDE_PANEL_TRANSITION_TIME)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => Shown?.Invoke());
        }

        public virtual void HideTweened()
        {
            RegisterAsHidden();

            _anchorPosTween.Kill();
            _anchorPosTween = _rectTransform
                .DOAnchorPosX(-_rectTransform.rect.size.y - 100, VisualManager.Instance.SIDE_PANEL_TRANSITION_TIME)
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
            transform.SetAsLastSibling();
            _isShowing = true;
            Shown?.Invoke();
        }

        private void RegisterAsHidden()
        {
            _isShowing = false;
            Hidden?.Invoke();
        }
    }
}