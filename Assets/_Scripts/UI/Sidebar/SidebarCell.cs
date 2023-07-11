using TMPro;
using UnityEngine;

namespace UI.Sidebar

{
    public class SidebarCell : MonoBehaviour
    {
        [SerializeField] private TMP_Text _cellTypeText;

        public void Init(string cellTypeText)
        {
            _cellTypeText.text = cellTypeText;
        }
    }
}