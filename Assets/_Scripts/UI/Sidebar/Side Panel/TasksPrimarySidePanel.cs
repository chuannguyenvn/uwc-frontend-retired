using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar
{
    public class TasksPrimarySidePanel : PrimarySidePanel
    {
        [SerializeField] private ScrollRect _scrollRect;

        public TasksPrimarySidePanel()
        {
            SidePanelType = SidePanelType.Tasks;
        }
    }
}