using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar
{
    public class SidePanelBackButton : MonoBehaviour
    {
        [SerializeField] private SidePanel _sidePanel;
        [SerializeField] private Button _button;

        private void Start()
        {
            _button.onClick.AddListener(HandleClick);
        }

        private void HandleClick()
        {
            _sidePanel.HideTweened();
        }
    }
}