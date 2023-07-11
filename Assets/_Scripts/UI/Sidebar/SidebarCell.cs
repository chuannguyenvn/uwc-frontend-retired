using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar

{
    public class SidebarCell : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _cellTypeText;

        private SidebarCellType _sidebarCellType;
        private SidebarPanel _sidebarPanel;

        public void Init(SidebarCellType sidebarCellType)
        {
            _sidebarCellType = sidebarCellType;

            _cellTypeText.text = SidebarManager.Instance.TypeNames[_sidebarCellType];
            _sidebarPanel = SidebarManager.Instance.Panels[_sidebarCellType];

            if (_sidebarPanel != null)
            {
                _button.onClick.AddListener(_sidebarPanel.Show);
            }
        }
    }
}