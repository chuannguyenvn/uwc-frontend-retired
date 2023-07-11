using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Sidebar
{
    public class SidebarManager : Singleton<SidebarManager>
    {
        [FormerlySerializedAs("mcpsSideInspector")] [FormerlySerializedAs("McpsSidebarPanel")] public McpsSidePanel mcpsSidePanel;

        public Dictionary<SideInspectorType, string> TypeNames;
        public Dictionary<SideInspectorType, SidePanel> Panels;

        public SideInspectorType CurrentlyActivatedSidebarType;

        public readonly Stack<SidePanel> BackStack = new();

        protected override void Awake()
        {
            base.Awake();
            
            TypeNames = new() {{SideInspectorType.Map, "Map"},{SideInspectorType.Mcps, "Mcps"},};
            Panels = new() {{SideInspectorType.Map, null}, {SideInspectorType.Mcps, mcpsSidePanel},};
        }

        public void PushBackStack(SidePanel sidePanel)
        {
            BackStack.Push(sidePanel);
        }
        
        public void PopBackStack()
        {
            BackStack.Pop();
        }
    }
}