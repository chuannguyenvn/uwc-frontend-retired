using DG.Tweening;
using Models;
using UI.Sidebar;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InformationPanel
{
    public class InformationPanel : MonoBehaviour
    {
        [SerializeField] protected Button _hideButton;
        [SerializeField] protected RectTransform _rectTransform;
        protected Tween _movementTween;
        protected bool _isShowing = true;
        protected Mcp _showingMcp;
        private float _initialWidth;

        protected virtual void Awake()
        {
            _initialWidth = _rectTransform.rect.width;
            _rectTransform = GetComponent<RectTransform>();
            _hideButton.onClick.AddListener(Hide);
        }

        protected virtual void Start()
        {
            SidebarController.Instance.SidePanelActivated += (_) => Hide();
            HideInstant();
        }

        public virtual void Hide()
        {
            if (!_isShowing) return;
            
            _movementTween?.Kill();
            _movementTween = _rectTransform.DOAnchorPosX(_initialWidth + 50, 0.15f).SetEase(Ease.OutCubic);
            _isShowing = false;
        }

        public virtual void HideInstant()
        {
            if (!_isShowing) return;

            _movementTween?.Kill();
            _rectTransform.anchoredPosition = _rectTransform.anchoredPosition.WithX(_initialWidth + 50);
            _isShowing = false;
        }
    }
}