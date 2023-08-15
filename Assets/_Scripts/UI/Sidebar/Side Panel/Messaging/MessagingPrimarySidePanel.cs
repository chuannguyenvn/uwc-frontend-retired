using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar.SidePanel.Messaging
{
    public class MessagingPrimarySidePanel : PrimarySidePanel
    {
        [SerializeField] private ScrollRect _scrollRect;

        public MessagingPrimarySidePanel()
        {
            SidePanelType = SidePanelType.Messaging;
        }

        protected override IEnumerator Start()
        {
            yield return base.Start();
        }
    }
}