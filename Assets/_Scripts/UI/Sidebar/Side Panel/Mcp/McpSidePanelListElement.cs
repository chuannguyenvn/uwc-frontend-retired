using Https;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI.ProceduralImage;

namespace UI.Sidebar
{
    public class McpSidePanelListElement : MonoBehaviour
    {
        [SerializeField] private ProceduralImage _backgroundImage;
        [SerializeField] private ProceduralImage _fillLevelImage;
        [SerializeField] private TMP_Text _addressText;
        [SerializeField] private TMP_Text _capacityText;

        public void Init(Mcp mcp)
        {
            _addressText.text = mcp.Latitude.ToString("F2") + ", " + mcp.Longitude.ToString("F2");
            _capacityText.text = "Capacity: " + mcp.CurrentLoad.ToString("F2") + "/" + mcp.Capacity.ToString("F2");
        }
    }
}