using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar
{
    public class McpsPrimarySidePanel : PrimarySidePanel
    {
        [SerializeField] private ScrollRect _scrollRect;

        public McpsPrimarySidePanel()
        {
            SidePanelType = SidePanelType.Mcps;
        }

        protected override void Start()
        {
            base.Start();
            
            
        }
    }
}