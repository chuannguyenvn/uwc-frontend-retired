using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar
{
    public class SidebarCell : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _cellTypeText;

        private SideInspectorType _sideInspectorType;
        private SidePanel _sidePanel;

        public void Init(SideInspectorType sideInspectorType)
        {
            _sideInspectorType = sideInspectorType;

            _cellTypeText.text = SidebarManager.Instance.TypeNames[_sideInspectorType];
            _sidePanel = SidebarManager.Instance.Panels[_sideInspectorType];

            if (_sidePanel != null)
            {
                _button.onClick.AddListener(TogglePanel);
            }
        }

        private void TogglePanel()
        {
            if (_sidePanel.IsShowing)
            {
                _sidePanel.HideTweened();
            }
            else
            {
                _sidePanel.ShowTweened();
            }
        }
    }
}