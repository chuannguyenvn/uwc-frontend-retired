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
        private SideInspector _sideInspector;

        private bool _isInspectorShowing = false;

        public void Init(SideInspectorType sideInspectorType)
        {
            _sideInspectorType = sideInspectorType;

            _cellTypeText.text = SidebarManager.Instance.TypeNames[_sideInspectorType];
            _sideInspector = SidebarManager.Instance.Panels[_sideInspectorType];

            if (_sideInspector != null)
            {
                _button.onClick.AddListener(ToggleInspector);
            }
        }

        private void ToggleInspector()
        {
            _isInspectorShowing = !_isInspectorShowing;
            if (_isInspectorShowing)
            {
                _sideInspector.ShowTweened();
            }
            else
            {
                _sideInspector.HideTweened();
            }
        }
    }
}