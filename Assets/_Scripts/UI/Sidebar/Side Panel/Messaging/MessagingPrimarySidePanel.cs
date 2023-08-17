using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Https;
using Models;
using UnityEngine;
using UnityEngine.UI;
using Managers;

namespace UI.Sidebar.SidePanel.Messaging
{
    public class MessagingPrimarySidePanel : PrimarySidePanel
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] RectTransform _inboxPanelRectTransform;

        private void Awake()
        {
            SidePanelType = SidePanelType.Messaging;
        }

        protected override IEnumerator Start()
        {
            var primaryPanelWidth = _rectTransform.rect.width;
            _inboxPanelRectTransform.sizeDelta = _inboxPanelRectTransform.sizeDelta.WithX(Screen.width - primaryPanelWidth - SidebarController.Instance.SidebarWidth);
            
            yield return base.Start();

            yield return HttpClient.SendRequest<List<Message>>(Endpoints.Message.InboxLatest(AuthenticationManager.Instance.UserId),
                HttpClient.RequestType.GET,
                (success, result) =>
                {
                    if (success) InitList(result);
                },
                "");
        }

        private void InitList(List<Message> messages)
        {
            foreach (var message in messages)
            {
                var element = SidePanelListElementPool.Instance.GetElement(_scrollRect.content);
                element.InitMessage(message);
            }
        }

        public override void ShowTweened()
        {
            base.ShowTweened();
            _inboxPanelRectTransform.DOAnchorPosX(0, 0.25f);
        }

        public override void HideTweened()
        {
            base.HideTweened();
            _inboxPanelRectTransform.DOAnchorPosX(-_inboxPanelRectTransform.rect.width, 0.25f);
        }

        public override void ShowInstant()
        {
            base.ShowInstant();
            _inboxPanelRectTransform.anchoredPosition = _inboxPanelRectTransform.anchoredPosition.WithX(0);
        }

        public override void HideInstant()
        {
            base.HideInstant();
            _inboxPanelRectTransform.anchoredPosition = _inboxPanelRectTransform.anchoredPosition.WithX(-_inboxPanelRectTransform.rect.width);
        }
    }
}