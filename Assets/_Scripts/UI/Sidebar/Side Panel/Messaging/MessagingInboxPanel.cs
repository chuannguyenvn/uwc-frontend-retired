using System.Collections.Generic;
using DG.Tweening;
using Https;
using Managers;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar.SidePanel.Messaging
{
    public class MessagingInboxPanel : Singleton<MessagingInboxPanel>
    {
        [SerializeField] MessageUnit _messageUnitPrefab;
        [SerializeField] ScrollRect _scrollRect;

        private List<MessageUnit> _messageUnits = new();

        public void Init(int otherUserId)
        {
            StartCoroutine(HttpClient.SendRequest<List<Message>>(
                Endpoints.Message.InboxWith(AuthenticationManager.Instance.UserId, otherUserId),
                HttpClient.RequestType.GET,
                (success, result) =>
                {
                    if (success) Init(result);
                },
                ""));
        }

        private void Init(List<Message> messages)
        {
            foreach (var messageUnit in _messageUnits)
            {
                Destroy(messageUnit.gameObject);
            }

            _messageUnits.Clear();

            foreach (var message in messages)
            {
                var messageUnit = Instantiate(_messageUnitPrefab, _scrollRect.content);
                messageUnit.Init(message.TextContent, message.TextTime, message.SenderAccount.Id == AuthenticationManager.Instance.UserId);
                _messageUnits.Add(messageUnit);
            }
            
            Canvas.ForceUpdateCanvases();

            foreach (var messageUnit in _messageUnits)
            {
                messageUnit.ReevaluateSize();
            }

            DOVirtual.DelayedCall(0.05f,
                () => _scrollRect.content.anchoredPosition = _scrollRect.content.anchoredPosition.WithY(_scrollRect.content.rect.height));
        }
    }
}