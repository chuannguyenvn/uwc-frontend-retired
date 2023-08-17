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
        protected bool _isShowing = false;
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

        public void Hide()
        {
            _movementTween?.Kill();
            _movementTween = _rectTransform.DOAnchorPosX(_initialWidth + 50, 0.15f).SetEase(Ease.OutCubic);
            _isShowing = false;
        }

        public void HideInstant()
        {
            _movementTween?.Kill();
            _rectTransform.anchoredPosition = _rectTransform.anchoredPosition.WithX(_initialWidth + 50);
            _isShowing = false;
        }
    }
}