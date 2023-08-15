using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar.SidePanel.Settings
{
    public class SettingsPrimarySidePanel : PrimarySidePanel
    {
        [SerializeField] private ScrollRect _scrollRect;

        private void Awake()
        {
            SidePanelType = SidePanelType.Settings;
        }

        protected override IEnumerator Start()
        {
            yield return base.Start();
        }
    }
}