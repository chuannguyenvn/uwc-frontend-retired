using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Sidebar
{
    public class SidebarController : Singleton<SidebarController>
    {
        [SerializeField] private TasksSidePanel _tasksSidePanel;
        [SerializeField] private McpsSidePanel _mcpsSidePanel;
        [SerializeField] private VehiclesSidePanel _vehiclesSidePanel;

        public Dictionary<SideInspectorType, string> TypeNames;
        public Dictionary<SideInspectorType, SidePanel> Panels;

        public SideInspectorType CurrentlyActivatedSidebarType;

        public readonly Stack<SidePanel> BackStack = new();

        protected override void Awake()
        {
            base.Awake();

            TypeNames = new()
            {
                {SideInspectorType.Map, "Map"},
                {SideInspectorType.Tasks, "Tasks"},
                {SideInspectorType.Workers, "Workers"},
                {SideInspectorType.Mcps, "Mcps"},
                {SideInspectorType.Vehicles, "Vehicles"},
                {SideInspectorType.Reports, "Reports"},
                {SideInspectorType.Messaging, "Messaging"},
                {SideInspectorType.Settings, "Settings"},
                {SideInspectorType.Helps, "Helps"},
            };
            
            Panels = new()
            {
                {SideInspectorType.Map, null},
                {SideInspectorType.Tasks, _tasksSidePanel},
                {SideInspectorType.Workers, null},
                {SideInspectorType.Mcps, _mcpsSidePanel},
                {SideInspectorType.Vehicles, null},
                {SideInspectorType.Reports, null},
                {SideInspectorType.Messaging, null},
                {SideInspectorType.Settings, null},
                {SideInspectorType.Helps, null},            };
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