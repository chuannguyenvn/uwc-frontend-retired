using System.Collections;
using System.Collections.Generic;
using Https;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar.SidePanel.Messaging
{
    public class MessagingPrimarySidePanel : PrimarySidePanel
    {
        [SerializeField] private ScrollRect _scrollRect;

        private void Awake()
        {
            SidePanelType = SidePanelType.Messaging;
        }

        protected override IEnumerator Start()
        {
            yield return base.Start();

            yield return HttpClient.SendRequest<List<Message>>(Endpoints.Message.INBOX_LATEST,
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
    }
}