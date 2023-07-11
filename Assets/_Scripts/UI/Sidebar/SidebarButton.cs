using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar
{
    public class SidebarCell : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _cellTypeText;

        private SidePanelType _sidePanelType;
        private PrimarySidePanel _primarySidePanel;

        public void Init(SidePanelType sidePanelType)
        {
            _sidePanelType = sidePanelType;

            _cellTypeText.text = SidebarController.Instance.TypeNames[_sidePanelType];
            _primarySidePanel = SidebarController.Instance.Panels[_sidePanelType];

            if (_primarySidePanel != null)
            {
                _button.onClick.AddListener(TogglePanel);
            }
        }

        private void TogglePanel()
        {
            if (!_primarySidePanel.IsShowing) SidebarController.Instance.PushBackStack(_primarySidePanel);
            else SidebarController.Instance.PopAllBackStack();
        }
    }
}