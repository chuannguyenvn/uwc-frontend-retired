using System;
using DG.Tweening;
using Models;
using TMPro;
using UI.Sidebar;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InformationPanel
{
    public class McpInformationPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _addressText;
        [SerializeField] private TMP_Text _fillLevelText;
        [SerializeField] private TMP_Text _emptyingLogText;
        [SerializeField] private Image _fillLevelBarImage;
        [SerializeField] private RectTransform _fillLevelBarRectTransform;
        [SerializeField] private Image _fillLevelDetailBackgroundImage;
        [SerializeField] private Button _hideButton;
        private RectTransform _rectTransform;
        private float _initialFillLevelBarWidth;
        private float _initialWidth;
        private Tween _movementTween;
        private bool _isShowing = false;
        private Mcp _showingMcp;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _initialFillLevelBarWidth = _fillLevelBarRectTransform.rect.width;
            _initialWidth = _rectTransform.rect.width;
            _hideButton.onClick.AddListener(() => Hide());
        }

        private void Start()
        {
            SidebarController.Instance.SidePanelActivated += (_) => Hide();
            HideInstant();
        }

        public void Show(Mcp mcp)
        {
            if (_showingMcp == mcp) return;
            _showingMcp = mcp;
         
            _addressText.text = mcp.Address;

            var fillPercentage = mcp.CurrentLoad / mcp.Capacity;
            var fillPercentageText = (fillPercentage * 100).ToString("F0");

            _fillLevelText.text = VisualManager.Instance.GetFillLevelDescription(fillPercentage) + "\n" + fillPercentageText + "%";

            _fillLevelBarRectTransform.sizeDelta = _fillLevelBarRectTransform.sizeDelta.WithX(_initialFillLevelBarWidth * fillPercentage);
            _fillLevelBarImage.color = VisualManager.Instance.GetFillLevelColor(fillPercentage);

            _fillLevelDetailBackgroundImage.color = VisualManager.Instance.GetFillLevelColor(fillPercentage);

            if (!_isShowing)
            {
                _movementTween?.Kill();
                _movementTween = _rectTransform.DOAnchorPosX(0, 0.15f).SetEase(Ease.OutCubic);
            }
            
            _isShowing = true;
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