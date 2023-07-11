using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Sidebar
{
    public class SidePanelBackButton : MonoBehaviour
    {
        [FormerlySerializedAs("_sidePanel")] [SerializeField] private PrimarySidePanel _primarySidePanel;
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