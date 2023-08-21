using System;
using UnityEngine;
using UnityEngine.UI;

namespace Visuals
{
    public class PanelVisualElement : MonoBehaviour
    {
        [SerializeField] private PanelType _panelType;
        private Image _image;
        
        private void Start()
        {
            _image = GetComponent<Image>();
            UpdateTheme();
            VisualManager.Instance.ThemeChanged += UpdateTheme;
        }

        private void UpdateTheme()
        {
            _image.color = _panelType switch
            {
                PanelType.Primary => VisualManager.Instance.PrimaryPanelColor,
                PanelType.Secondary => VisualManager.Instance.SecondaryPanelColor,
                PanelType.Background => VisualManager.Instance.BackgroundPanelColor,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}