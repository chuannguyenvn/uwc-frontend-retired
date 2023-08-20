using System.Collections;
using System.Collections.Generic;
using Commons.Types;
using Https;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar.SidePanel.Workers
{
    public class WorkersPrimarySidePanel : PrimarySidePanel
    {
        [SerializeField] private ScrollRect _scrollRect;

        private void Awake()
        {
            SidePanelType = SidePanelType.Workers;
        }

        protected override IEnumerator Start()
        {
            yield return base.Start();

            yield return HttpClient.SendRequest<List<UserProfile>>(Endpoints.UserProfile.GET_ALL,
                HttpClient.RequestType.GET,
                (success, result) =>
                {
                    if (success) InitList(result);
                },
                "");
        }

        private void InitList(List<UserProfile> userProfiles)
        {
            foreach (var userProfile in userProfiles)
            {
                if (userProfile.Role != UserRole.Driver) continue;
                var element = SidePanelListElementPool.Instance.GetElement(_scrollRect.content);
                element.InitWorker(userProfile);
            }
        }
    }
}