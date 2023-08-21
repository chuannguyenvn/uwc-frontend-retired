using System;
using TMPro;
using UnityEngine;

namespace Visuals
{
    public class TextVisualElement : MonoBehaviour
    {
        [SerializeField] private FontType _fontType;

        private void Start()
        {
            UpdateFont();
            VisualManager.Instance.ThemeChanged += UpdateFont;
        }

        private void UpdateFont()
        {
            var text = GetComponent<TMP_Text>();
            text.fontStyle = FontStyles.Normal;
            switch (_fontType)
            {
                case FontType.Primary:
                    text.font = VisualManager.Instance.PrimaryTextFont;
                    text.color = VisualManager.Instance.IsDarkMode ? Color.white : VisualManager.Instance.PrimaryTextColor;
                    break;
                case FontType.Secondary:
                    text.font = VisualManager.Instance.SecondaryTextFont;
                    text.color = VisualManager.Instance.IsDarkMode ? Color.white : VisualManager.Instance.SecondaryTextColor;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}