using System;
using DG.Tweening;
using Models;
using TMPro;
using UI.Sidebar;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InformationPanel
{
    public class McpInformationPanel : InformationPanel
    {
        [SerializeField] private TMP_Text _addressText;
        [SerializeField] private TMP_Text _fillLevelText;
        [SerializeField] private TMP_Text _emptyingLogText;
        [SerializeField] private Image _fillLevelBarImage;
        [SerializeField] private RectTransform _fillLevelBarRectTransform;
        [SerializeField] private Image _fillLevelDetailBackgroundImage;
        private float _initialFillLevelBarWidth;
        
        protected override void Awake()
        {
            base.Awake();
            _initialFillLevelBarWidth = _fillLevelBarRectTransform.rect.width;
        }

        public override void Show(Mcp mcp)
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

     
    }
}