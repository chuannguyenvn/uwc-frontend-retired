using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar.SidePanel.Vehicles
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