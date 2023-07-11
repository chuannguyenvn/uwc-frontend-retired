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
            var text = GetComponent<TMP_Text>();
            switch (_fontType)
            {
                case FontType.Primary:
                    text.font = VisualManager.Instance.PrimaryFont;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}