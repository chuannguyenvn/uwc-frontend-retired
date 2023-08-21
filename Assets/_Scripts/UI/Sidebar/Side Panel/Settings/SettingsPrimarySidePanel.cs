using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Sidebar.SidePanel.Settings
{
    public class SettingsPrimarySidePanel : PrimarySidePanel
    {
        [SerializeField] private ScrollRect _scrollRect;

        [SerializeField] private List<Button> _themeButtons;
        [SerializeField] private Button _darkModeButton;
        [SerializeField] private TMP_Text _darkModeButtonText;

        private void Awake()
        {
            SidePanelType = SidePanelType.Settings;
        }

        protected override IEnumerator Start()
        {
            yield return base.Start();

            _darkModeButton.onClick.AddListener(VisualManager.Instance.ToggleDarkMode);
            VisualManager.Instance.DarkModeToggled += isDarkMode => _darkModeButtonText.text = isDarkMode ? "On" : "Off";

            for (var i = 0; i < _themeButtons.Count; i++)
            {
                var button = _themeButtons[i];
                var index = i;
                button.targetGraphic.color = VisualManager.Instance.Themes[index];
                button.onClick.AddListener(() => VisualManager.Instance.SetTheme(index));
            }
        }
    }
}