using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar.SidePanel
{
    public class SidePanelBackButton : MonoBehaviour
    {
        [SerializeField] private PrimarySidePanel _primarySidePanel;
        [SerializeField] private Button _button;

        private void Start()
        {
            _button.onClick.AddListener(HandleClick);
        }

        private void HandleClick()
        {
            SidebarController.Instance.PopBackStack();
        }
    }
}