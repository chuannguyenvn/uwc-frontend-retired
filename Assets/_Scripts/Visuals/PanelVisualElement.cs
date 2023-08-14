using System;
using UnityEngine;
using UnityEngine.UI;

namespace Visuals
{
    public class PanelVisualElement : MonoBehaviour
    {
        [SerializeField] private PanelType _panelType;

        private void Start()
        {
            var image = GetComponent<Image>();
            image.color = _panelType switch
            {
                PanelType.Primary => VisualManager.Instance.PrimaryPanelColor,
                PanelType.Secondary => VisualManager.Instance.SecondaryPanelColor,
                PanelType.Background => VisualManager.Instance.BackgroundPanelColor,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}