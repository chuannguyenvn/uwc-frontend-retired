using System.Collections.Generic;
using UnityEngine;

namespace UI.Sidebar
{
    public class SidebarManager : Singleton<SidebarManager>
    {
        public McpsSidebarPanel McpsSidebarPanel;

        public Dictionary<SidebarCellType, string> TypeNames;
        public Dictionary<SidebarCellType, SidebarPanel> Panels;

        protected override void Awake()
        {
            base.Awake();
            
            TypeNames = new() {{SidebarCellType.Map, "Map"},{SidebarCellType.Mcps, "Mcps"},};
            Panels = new() {{SidebarCellType.Map, null}, {SidebarCellType.Mcps, McpsSidebarPanel},};
        }
    }
}