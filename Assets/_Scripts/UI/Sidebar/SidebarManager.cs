using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Sidebar
{
    public class SidebarManager : Singleton<SidebarManager>
    {
        [FormerlySerializedAs("McpsSidebarPanel")] public McpsSideInspector mcpsSideInspector;

        public Dictionary<SideInspectorType, string> TypeNames;
        public Dictionary<SideInspectorType, SideInspector> Panels;

        public SideInspectorType CurrentlyActivatedSidebarType;
        
        protected override void Awake()
        {
            base.Awake();
            
            TypeNames = new() {{SideInspectorType.Map, "Map"},{SideInspectorType.Mcps, "Mcps"},};
            Panels = new() {{SideInspectorType.Map, null}, {SideInspectorType.Mcps, mcpsSideInspector},};
        }
    }
}