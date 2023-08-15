using Models;
using UnityEngine;

namespace UI.InformationPanel
{
    public class InformationPanelController : Singleton<InformationPanelController>
    {
        [SerializeField] private McpInformationPanel _mcpInformationPanel;

        public void ShowMcpPanel(Mcp mcp)
        {
            _mcpInformationPanel.Show(mcp);
        }
    }
}