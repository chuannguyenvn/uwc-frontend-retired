using System;
using System.Collections;
using System.Collections.Generic;
using Https;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar.SidePanel
{
    public class McpsPrimarySidePanel : PrimarySidePanel
    {
        [SerializeField] private ScrollRect _scrollRect;

        private void Awake()
        {
            SidePanelType = SidePanelType.Mcps;
        }

        protected override IEnumerator Start()
        {
            yield return base.Start();

            yield return HttpClient.SendRequest<List<Mcp>>(Endpoints.Mcp.GET_ALL,
                HttpClient.RequestType.GET,
                (success, result) =>
                {
                    if (success) InitList(result);
                },
                "");
        }

        private void InitList(List<Mcp> mcps)
        {
            foreach (var mcp in mcps)
            {
                var element = SidePanelListElementPool.Instance.GetElement(_scrollRect.content);
                element.InitMcp(mcp);
            }
        }
    }
}