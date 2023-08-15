using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar.SidePanel.Workers
{
    public class WorkersPrimarySidePanel : PrimarySidePanel
    {
        [SerializeField] private ScrollRect _scrollRect;

        public WorkersPrimarySidePanel()
        {
            SidePanelType = SidePanelType.Workers;
        }

        protected override IEnumerator Start()
        {
            yield return base.Start();
        }
    }
}