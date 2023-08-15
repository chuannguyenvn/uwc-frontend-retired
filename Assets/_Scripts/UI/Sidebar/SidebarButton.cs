using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar
{
    public class SidebarCell : MonoBehaviour
    {
        [SerializeField] private List<Sprite> _iconSprites;

        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _cellTypeText;
        private PrimarySidePanel _primarySidePanel;

        private SidePanelType _sidePanelType;

        public void Init(SidePanelType sidePanelType)
        {
            _sidePanelType = sidePanelType;

            _icon.sprite = sidePanelType switch
            {
                SidePanelType.Map => _iconSprites[0],
                SidePanelType.Workers => _iconSprites[1],
                SidePanelType.Mcps => _iconSprites[2],
                SidePanelType.Vehicles => _iconSprites[3],
                SidePanelType.Reports => _iconSprites[4],
                SidePanelType.Messaging => _iconSprites[5],
                SidePanelType.Settings => _iconSprites[6],
                _ => throw new ArgumentOutOfRangeException(nameof(sidePanelType), sidePanelType, null)
            };

            _cellTypeText.text = SidebarController.Instance.TypeNames[_sidePanelType];
            _primarySidePanel = SidebarController.Instance.Panels[_sidePanelType];

            if (_primarySidePanel != null) _button.onClick.AddListener(TogglePanel);
        }

        private void TogglePanel()
        {
            if (!_primarySidePanel.IsShowing) SidebarController.Instance.PushBackStack(_primarySidePanel);
            else SidebarController.Instance.PopAllBackStack();
        }
    }
}