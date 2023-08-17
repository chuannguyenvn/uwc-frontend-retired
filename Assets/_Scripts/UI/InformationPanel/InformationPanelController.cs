using Models;
using UnityEngine;

namespace UI.InformationPanel
{
    public class InformationPanelController : Singleton<InformationPanelController>
    {
        [SerializeField] private McpInformationPanel _mcpInformationPanel;
        [SerializeField] private VehicleInformationPanel _vehicleInformationPanel;

        public void ShowMcpPanel(Mcp mcp)
        {
            _mcpInformationPanel.Show(mcp);
        }
        
        public void ShowVehiclePanel(Vehicle vehicle)
        {
            _vehicleInformationPanel.Show(vehicle);
        }
    }
}