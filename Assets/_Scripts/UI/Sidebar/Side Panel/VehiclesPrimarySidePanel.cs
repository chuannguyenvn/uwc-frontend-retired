using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar
{
    public class VehiclesPrimarySidePanel : PrimarySidePanel
    {
        [SerializeField] private ScrollRect _scrollRect;

        public VehiclesPrimarySidePanel()
        {
            SidePanelType = SidePanelType.Vehicles;
        }
    }
}