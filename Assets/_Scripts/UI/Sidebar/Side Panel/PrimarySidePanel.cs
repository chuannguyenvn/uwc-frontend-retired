using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.Sidebar
{
    public abstract class PrimarySidePanel : MonoBehaviour
    {
        public SidePanelType SidePanelType;

        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private TMP_Text _titleText;
        
        private Tween _anchorPosTween;

        public bool IsShowing { get; private set; }

        protected virtual IEnumerator Start()
        {
            _titleText.text = SidebarController.Instance.TypeNames[SidePanelType];
            
            ShowInstant();
            HideInstant();
            
            yield break;
        }

        public event Action Shown;
        public event Action Hidden;

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
            IsShowing = true;
            Shown?.Invoke();
        }

        private void RegisterAsHidden()
        {
            IsShowing = false;
            Hidden?.Invoke();
        }
    }
}