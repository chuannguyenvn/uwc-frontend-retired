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
            _addressText.text = mcp.Latitude + ", " + mcp.Longitude;
            _capacityText.text = "Capacity: " + mcp.CurrentLoad + "/" + mcp.Capacity;
        }
    }
}