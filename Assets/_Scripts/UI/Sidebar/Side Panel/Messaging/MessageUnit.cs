using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Sidebar.SidePanel.Messaging
{
    public class MessageUnit : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _messageBoxRectTransform;
        [SerializeField] private RectTransform _messageTextRectTransform;
        [SerializeField] private TMP_Text _messageText;
        [SerializeField] private Image _messageBackgroundImage;
        [SerializeField] private TMP_Text _timestampText;
        private float _initialWitdh;
        private bool _isFromCurrentUser;

        private void Start()
        {
            _initialWitdh = _messageBoxRectTransform.rect.width;
        }

        public void Init(string messageContent, DateTime timestamp, bool isFromCurrentUser)
        {
            _isFromCurrentUser = isFromCurrentUser;

            if (isFromCurrentUser)
            {
                _messageText.alignment = _timestampText.alignment = TextAlignmentOptions.TopRight;
                _messageText.color = Color.white;
                _messageBackgroundImage.color = VisualManager.Instance.PrimaryPanelColor;
            }
            else
            {
                _messageText.alignment = _timestampText.alignment = TextAlignmentOptions.TopLeft;
                _messageText.color = VisualManager.Instance.PrimaryTextColor;
                _messageBackgroundImage.color = VisualManager.Instance.BackgroundPanelColor;
            }

            _messageText.text = messageContent;
            _timestampText.text = TimeZoneInfo.ConvertTimeFromUtc(timestamp, TimeZoneInfo.Local).ToString("HH:mm");
        }

        public void ReevaluateSize()
        {
            StartCoroutine(ReevaluateSize_CO());
            // _messageBoxRectTransform.sizeDelta =
            //     _messageBoxRectTransform.sizeDelta.WithX((_isFromCurrentUser ? 1 : -1) * (_initialWitdh - _messageText.preferredWidth));
        }

        private IEnumerator ReevaluateSize_CO()
        {
            yield return null;
            var backgroundRectTransform = _messageBackgroundImage.rectTransform;
            backgroundRectTransform.pivot = new Vector2(_isFromCurrentUser ? 1 : 0, 0.5f);
            backgroundRectTransform.anchorMin = new Vector2(_isFromCurrentUser ? 1 : 0, 0);
            backgroundRectTransform.anchorMax = new Vector2(_isFromCurrentUser ? 1 : 0, 1);
            backgroundRectTransform.sizeDelta = backgroundRectTransform.sizeDelta.WithX(_messageText.preferredWidth + 30);
            _rectTransform.sizeDelta = _rectTransform.sizeDelta.WithY(_messageText.preferredHeight + 20 + 50);
        }
    }
}