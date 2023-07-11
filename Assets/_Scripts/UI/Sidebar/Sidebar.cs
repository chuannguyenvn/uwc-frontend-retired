using System;
using Managers;
using UnityEngine;

namespace UI.Sidebar
{
    public class Sidebar : MonoBehaviour
    {
        private void Start()
        {
            var cellPrefab = ResourceManager.Instance.SidebarCellPrefab;
            foreach (var type in Enum.GetValues(typeof(SideInspectorType)))
            {
                var cell = Instantiate(cellPrefab, transform, false);
                cell.Init((SideInspectorType)type);
            }
        }
    }
}