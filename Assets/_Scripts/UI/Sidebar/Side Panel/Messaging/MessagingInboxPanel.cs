using System;
using System.Collections.Generic;
using Communications.Message;
using DG.Tweening;
using Https;
using Managers;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar.SidePanel.Messaging
{
    public class MessagingInboxPanel : Singleton<MessagingInboxPanel>
    {
        [SerializeField] private MessageUnit _messageUnitPrefab;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private TMP_InputField _messageInputField;
        [SerializeField] private Button _sendButton;
        private List<MessageUnit> _messageUnits = new();
        private int _otherUserId;

        private void Start()
        {
            _messageInputField.onSubmit.AddListener(SendMessage);
            _sendButton.onClick.AddListener(() => SendMessage(_messageInputField.text));
        }

        public void Init(int otherUserId)
        {
            _otherUserId = otherUserId;

            StartCoroutine(HttpClient.SendRequest<List<Message>>(
                Endpoints.Message.InboxWith(AuthenticationManager.Instance.UserId, otherUserId),
                HttpClient.RequestType.GET,
                (success, result) =>
                {
                    if (success) InitMessageList(result);
                },
                ""));
        }

        private void InitMessageList(List<Message> messages)
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

        private void SendMessage(string messageContent)
        {
            _messageInputField.text = "";
            _messageInputField.ActivateInputField();
            
            StartCoroutine(HttpClient.SendRequest(Endpoints.Message.ADD,
                HttpClient.RequestType.POST,
                (success) =>
                {
                    if (success) Init(_otherUserId);
                },
                "",
                new AddMessageRequest
                {
                    Sender = AuthenticationManager.Instance.UserId,
                    Receiver = _otherUserId,
                    TextTime = DateTime.UtcNow,
                    TextContent = messageContent
                }));
        }
    }
}