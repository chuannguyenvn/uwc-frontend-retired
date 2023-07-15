using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar
{
    public class SettingsPrimarySidePanel : PrimarySidePanel
    {
        [SerializeField] private ScrollRect _scrollRect;

        public SettingsPrimarySidePanel()
        {
            SidePanelType = SidePanelType.Settings;
        }

        protected override void Start()
        {
            base.Start();
            
            
        }
    }
}