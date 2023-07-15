using System.Collections;
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

        protected override IEnumerator Start()
        {
            yield return base.Start();
        }
    }
}