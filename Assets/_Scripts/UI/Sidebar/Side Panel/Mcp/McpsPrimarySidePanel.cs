using System.Collections;
using System.Collections.Generic;
using Https;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar.SidePanel.Mcp
{
    public class McpsPrimarySidePanel : PrimarySidePanel
    {
        [SerializeField] private McpSidePanelListElement _listElementPrefab;
        [SerializeField] private ScrollRect _scrollRect;

        public McpsPrimarySidePanel()
        {
            SidePanelType = SidePanelType.Mcps;
        }

        protected override IEnumerator Start()
        {
            yield return base.Start();

            yield return HttpClient.SendRequest<List<Models.Mcp>>(Endpoints.Mcp.GET_ALL,
                HttpClient.RequestType.GET,
                (success, result) =>
                {
                    if (success) InitList(result);
                },
                "");
        }

        private void InitList(List<Models.Mcp> mcps)
        {
            foreach (var mcp in mcps)
            {
                var element = Instantiate(_listElementPrefab, _scrollRect.content, false);
                element.Init(mcp);
            }
        }
    }
}