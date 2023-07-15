using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar
{
    public class MessagingPrimarySidePanel : PrimarySidePanel
    {
        [SerializeField] private ScrollRect _scrollRect;

        public MessagingPrimarySidePanel()
        {
            SidePanelType = SidePanelType.Messaging;
        }

        protected override void Start()
        {
            base.Start();
            
            
        }
    }
}